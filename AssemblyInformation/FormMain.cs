using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using AssemblyInformation.Model;

namespace AssemblyInformation
{
    public partial class FormMain : Form
    {
        private const string Loading = "Loading";

        private string _assemblyPath;

        private static readonly Dictionary<string, Form> AssemblyFormMap = new Dictionary<string, Form>();

        private AssemblyInformationLoader assemblyInformation;

        private List<Binary> recursiveDependencies;

        private List<Binary> directDependencies;

        private Label dropHintLabel;

        public FormMain(string assemblyPath)
        {
            InitializeComponent();
            FormClosing += FormMainFormClosing;
            UpdateThemeCheckmarks();

            dropHintLabel = new Label
            {
                Text = "Drop a .dll or .exe here\nor use Options \u2192 Open Assembly (Ctrl+O)",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = SystemColors.GrayText,
                Font = new Font(Font.FontFamily, 14f),
                AutoSize = false
            };
            Controls.Add(dropHintLabel);

            if (assemblyPath != null)
                LoadAssembly(assemblyPath);
        }

        private void LoadAssembly(string assemblyPath)
        {
            // Clean up previous state
            if (_assemblyPath != null)
                AssemblyFormMap.Remove(_assemblyPath);
            directDependencies = null;
            recursiveDependencies = null;
            dependencyTreeView.Nodes.Clear();
            referenceListListBox.Items.Clear();
            referringAssembliesListtBox.Items.Clear();
            versionInfoListView.Items.Clear();

            _assemblyPath = assemblyPath;
            try
            {
                assemblyInformation = new AssemblyInformationLoader(assemblyPath);

                // If this is a .NET apphost exe, automatically load the companion managed dll
                if (assemblyInformation.ApphostManagedDll != null)
                {
                    _assemblyPath = assemblyInformation.ApphostManagedDll;
                    assemblyInformation = new AssemblyInformationLoader(_assemblyPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading assembly: {ex.Message}", Resource.AppName,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            referringAssemblyFolderTextBox.Text = Path.GetDirectoryName(_assemblyPath);
            AssemblyFormMap[_assemblyPath] = this;
            Text = $"Assembly Information - {Path.GetFileName(_assemblyPath)}";
            panel1.Visible = true;
            dropHintLabel.Visible = false;

            // Only call FormMainLoad directly if the form is already shown.
            // On first construction, the Form.Load event will call it.
            if (IsHandleCreated)
                FormMainLoad(this, EventArgs.Empty);
        }

        private void FormMainLoad(object sender, EventArgs e)
        {
            // If no assembly loaded, show empty state
            if (assemblyInformation == null)
            {
                panel1.Visible = false;
                dropHintLabel.Visible = true;
                Text = "Assembly Information";
                return;
            }

            string debuggableFlagsToolTipText;

            // Prevent read-only display fields from receiving focus
            txtTrackingEnabled.TabStop = false;
            txtOptimized.TabStop = false;
            txtSequencing.TabStop = false;
            txtEditAndContinue.TabStop = false;
            frameWorkVersion.TabStop = false;
            assemblyKindTextBox.TabStop = false;
            targetProcessorTextBox.TabStop = false;
            txtFullName.TabStop = false;

            assemblyKindTextBox.Text = assemblyInformation.AssemblyKind;
            targetProcessorTextBox.Text = assemblyInformation.TargetProcessor;
            frameWorkVersion.Text = assemblyInformation.FrameworkVersion;
            txtFullName.Text = assemblyInformation.AssemblyFullName;

            FillVersionInfo();

            if (!assemblyInformation.IsManaged)
            {
                // Native binary - show what we have, disable .NET-specific fields
                debuggableFlagsToolTipText = @"Debugging Flags: N/A (native binary)";
                DebuggableFlagsToolTip.Tag = debuggableFlagsToolTipText;
                txtTrackingEnabled.Text = "N/A";
                txtTrackingEnabled.BackColor = SystemColors.Control;
                txtTrackingEnabled.ForeColor = SystemColors.GrayText;
                txtOptimized.Text = "N/A";
                txtOptimized.BackColor = SystemColors.Control;
                txtOptimized.ForeColor = SystemColors.GrayText;
                txtSequencing.Text = "N/A";
                txtSequencing.BackColor = SystemColors.Control;
                txtSequencing.ForeColor = SystemColors.GrayText;
                txtEditAndContinue.Text = "N/A";
                txtEditAndContinue.BackColor = SystemColors.Control;
                txtEditAndContinue.ForeColor = SystemColors.GrayText;
                // Show Version Info tab by default for native files (references are empty)
                tabControl1.SelectedTab = tabPage4;
                return;
            }

            if (assemblyInformation.DebuggingFlags != null)
            {
                debuggableFlagsToolTipText = string.Format(@"Debugging Flags: {0}", assemblyInformation.DebuggingFlags);
            }
            else
            {
                debuggableFlagsToolTipText = @"Debugging Flags: NONE";
            }
            DebuggableFlagsToolTip.Tag = debuggableFlagsToolTipText;

            // Display values
            if (assemblyInformation.JitTrackingEnabled)
            {
                txtTrackingEnabled.Text = "Debug";
                txtTrackingEnabled.BackColor = Color.Red;
                txtTrackingEnabled.ForeColor = Color.White;
            }
            else
            {
                txtTrackingEnabled.Text = "Release";
                txtTrackingEnabled.BackColor = Color.Green;
                txtTrackingEnabled.ForeColor = Color.White;
            }

            if (assemblyInformation.JitOptimized)
            {
                txtOptimized.Text = "Optimized";
                txtOptimized.BackColor = Color.Green;
                txtOptimized.ForeColor = Color.White;
            }
            else
            {
                txtOptimized.Text = "Not Optimized";
                txtOptimized.BackColor = Color.Red;
                txtOptimized.ForeColor = Color.White;
            }

            if (assemblyInformation.IgnoreSymbolStoreSequencePoints)
            {
                txtSequencing.Text = "MSIL Sequencing";
                txtSequencing.BackColor = Color.Green;
                txtSequencing.ForeColor = Color.White;
            }
            else
            {
                txtSequencing.Text = "PDB Sequencing";
                txtSequencing.BackColor = assemblyInformation.JitTrackingEnabled ? Color.Red : Color.Orange;
                txtSequencing.ForeColor = Color.White;
            }

            if (assemblyInformation.EditAndContinueEnabled)
            {
                txtEditAndContinue.Text = "Edit and Continue Enabled";
                txtEditAndContinue.BackColor = Color.Red;
                txtEditAndContinue.ForeColor = Color.White;
            }
            else
            {
                txtEditAndContinue.Text = "Edit and Continue Disabled";
                txtEditAndContinue.BackColor = Color.Green;
                txtEditAndContinue.ForeColor = Color.White;
            }

            // Reset to first tab for managed assemblies
            tabControl1.SelectedIndex = 0;

            DependencyWalker dependencyWalker = new DependencyWalker();
            directDependencies = dependencyWalker.FindDependencies(_assemblyPath, false, out _).ToList();

            FillAssemblyReferences(directDependencies);
        }

        private void FillVersionInfo()
        {
            versionInfoListView.Items.Clear();
            var vi = assemblyInformation.VersionInfo;
            if (vi == null) return;

            void AddItem(string property, string value)
            {
                if (!string.IsNullOrEmpty(value))
                    versionInfoListView.Items.Add(new ListViewItem(new[] { property, value }));
            }

            AddItem("File Description", vi.FileDescription);
            AddItem("File Version", vi.FileVersion);
            AddItem("Product Name", vi.ProductName);
            AddItem("Product Version", vi.ProductVersion);
            AddItem("Company Name", vi.CompanyName);
            AddItem("Legal Copyright", vi.LegalCopyright);
            AddItem("Legal Trademarks", vi.LegalTrademarks);
            AddItem("Original Filename", vi.OriginalFilename);
            AddItem("Internal Name", vi.InternalName);
            AddItem("Comments", vi.Comments);
            AddItem("Language", vi.Language);
            AddItem("Private Build", vi.PrivateBuild);
            AddItem("Special Build", vi.SpecialBuild);

            if (vi.IsDebug) AddItem("Is Debug", "Yes");
            if (vi.IsPatched) AddItem("Is Patched", "Yes");
            if (vi.IsPreRelease) AddItem("Is Pre-Release", "Yes");
            if (vi.IsPrivateBuild) AddItem("Is Private Build", "Yes");
            if (vi.IsSpecialBuild) AddItem("Is Special Build", "Yes");
        }

        private void FormMainFormClosing(object sender, FormClosingEventArgs e)
        {
            if (_assemblyPath != null)
                AssemblyFormMap.Remove(_assemblyPath);
        }

        private void FillAssemblyReferences(IEnumerable<Binary> dependencies, TreeNode treeNode = null)
        {
            foreach (var binary in dependencies)
            {
                if (hideGACAssembliesToolStripMenuItem.Checked && binary.IsSystemBinary) continue;
                string assemblyName = showAssemblyFullNameToolStripMenuItem.Checked
                                          ? binary.FullName
                                          : binary.DisplayName;
                TreeNode node = new TreeNode(assemblyName);
                node.Tag = binary;

                if (treeNode == null)
                {
                    dependencyTreeView.Nodes.Add(node);
                }
                else
                {
                    if (!FindNodeParent(treeNode, assemblyName))
                    {
                        treeNode.Nodes.Add(node);
                    }
                    else
                    {
                        Trace.WriteLine(String.Format("{0} is already a parent of {1}", assemblyName, treeNode.Name));
                    }
                }
                if (!binary.IsSystemBinary)
                {
                    // Add dummy child to show expand icon; resolved lazily on expand
                    node.Nodes.Add(new TreeNode(Loading));
                }
            }
        }

        private void AboutToolStripMenuItem1Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
        }

        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Close();
        }

        private void LblCompilationMouseEnter(object sender, EventArgs e)
        {
            DebuggableFlagsToolTip.Active = true;  // workaround for MS bug
            DebuggableFlagsToolTip.Show((string)DebuggableFlagsToolTip.Tag, lblCompilation);
        }

        private void LblCompilationMouseLeave(object sender, EventArgs e)
        {
            DebuggableFlagsToolTip.Active = false;
        }

        private static bool FindNodeParent(TreeNode node, string parentName)
        {
            if (null == node) return false;
            if (null == parentName) return false;

            while (node != null)
            {
                if (node.Text.Equals(parentName))
                    return true;
                node = node.Parent;
            }

            return false;
        }

        private void DependencyTreeViewMouseDoubleClick(object sender, MouseEventArgs e)
        {
            var node = dependencyTreeView.SelectedNode;
            if (null != node)
            {
                Binary binary = node.Tag as Binary;
                if (binary?.FullPath != null)
                {
                    OpenAssemblyInformation(binary.FullPath);
                }
            }
        }

        private static void OpenAssemblyInformation(string assemblyPath)
        {
            if (!File.Exists(assemblyPath))
                return;

            if (!AssemblyFormMap.ContainsKey(assemblyPath))
            {
                try
                {
                    var form = new FormMain(assemblyPath);
                    form.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(Resource.LoadError, ex.Message), Resource.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                AssemblyFormMap[assemblyPath].BringToFront();
            }
        }

        private void DependencyTreeViewBeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (null != e && null != e.Node && e.Node.Nodes.Count == 1 && e.Node.Nodes[0].Text == Loading)
            {
                e.Node.Nodes.Clear();
                Binary binary = e.Node.Tag as Binary;
                if (binary == null) return;

                if (AssemblyInformationLoader.SystemAssemblies.Where(p => binary.FullName.StartsWith(p)).Count() > 0)
                {
                    e.Node.Nodes.Clear();
                    return;
                }

                // Resolve the assembly path using the full NuGet-aware resolver
                var resolvedPath = ResolveReferencePath(binary);
                DependencyWalker dependencyWalker = new DependencyWalker();
                if (resolvedPath != null)
                {
                    var referredAssemblies = dependencyWalker.FindDependencies(resolvedPath, false, out _);
                    FillAssemblyReferences(referredAssemblies, e.Node);
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (assemblyInformation == null || !assemblyInformation.IsManaged) return;

            if (tabControl1.SelectedIndex == 1 && referenceListListBox.Items.Count == 0)
            {
                FillRecursiveDependency();
            }
        }

        private void FillRecursiveDependency()
        {
            if (null == recursiveDependencies)
            {
                DependencyWalker dependencyWalker = new DependencyWalker();
                List<string> errors;
                System.Windows.Forms.Cursor existingCursor = Cursor;
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    recursiveDependencies = dependencyWalker.FindDependencies(_assemblyPath, true, out errors).ToList();
                }
                finally
                {
                    this.Cursor = existingCursor;
                }

                referenceListListBox.Items.Clear();
                foreach (var dependency in recursiveDependencies)
                {
                    if (hideGACAssembliesToolStripMenuItem.Checked && dependency.IsSystemBinary)
                        continue;
                    referenceListListBox.Items.Add(showAssemblyFullNameToolStripMenuItem.Checked ?
                        dependency.FullName :
                        dependency.DisplayName);
                }
                if (errors.Count > 0)
                {
                    referenceListListBox.Items.Add("");
                    referenceListListBox.Items.Add("-----------------ERRORS--------------");
                    foreach (string error in errors)
                    {
                        referenceListListBox.Items.Add(error);
                    }
                }
            }
            else
            {
                referenceListListBox.Items.Clear();
                foreach (var dependency in recursiveDependencies)
                {
                    if (hideGACAssembliesToolStripMenuItem.Checked && dependency.IsSystemBinary)
                        continue;
                    referenceListListBox.Items.Add(showAssemblyFullNameToolStripMenuItem.Checked ?
                        dependency.FullName :
                        dependency.DisplayName);
                }
            }
        }

        private void referringAssemblyFolderSearchButton_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(referringAssemblyFolderTextBox.Text) &&
                Directory.Exists(referringAssemblyFolderTextBox.Text))
            {
                FindReferringAssembliesForm frm = new FindReferringAssembliesForm();
                frm.DirectoryPath = referringAssemblyFolderTextBox.Text;
                frm.TestAssemblyFullName = assemblyInformation.AssemblyFullName;
                frm.Recursive = true;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var binaries = frm.ReferringAssemblies;
                    if (null == binaries) return;
                    referringAssembliesListtBox.Items.Clear();
                    foreach (var binary in binaries)
                    {
                        referringAssembliesListtBox.Items.Add(binary);
                    }
                }
            }
        }

        private void referringAssemblyBrowseFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                referringAssemblyFolderTextBox.Text = dlg.SelectedPath;
            }
        }

        private void AssemblyListBoxMouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            if (null == listBox || null == listBox.SelectedItem) return;

            var selectedAssembly = listBox.SelectedItem.ToString();
            if (!selectedAssembly.Contains("ERROR"))
            {
                // Try to resolve the assembly name to a file path in the same directory
                var searchDir = Path.GetDirectoryName(_assemblyPath);
                var parts = selectedAssembly.Split(',');
                if (parts.Length > 0)
                {
                    var dllPath = Path.Combine(searchDir, parts[0].Trim() + ".dll");
                    if (File.Exists(dllPath))
                    {
                        OpenAssemblyInformation(dllPath);
                    }
                }
            }
        }

        private void hideGACAssembliesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hideGACAssembliesToolStripMenuItem.Checked = !hideGACAssembliesToolStripMenuItem.Checked;
            RefreshDependencyDisplay();
        }

        private void showAssemblyFullNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showAssemblyFullNameToolStripMenuItem.Checked =
                !showAssemblyFullNameToolStripMenuItem.Checked;
            RefreshDependencyDisplay();
        }

        private void RefreshDependencyDisplay()
        {
            dependencyTreeView.Nodes.Clear();
            if (directDependencies != null)
                FillAssemblyReferences(directDependencies);
            if (recursiveDependencies != null)
                FillRecursiveDependency();
        }

        private void OpenAssemblyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var dlg = new OpenFileDialog
            {
                Title = "Open Assembly",
                Filter = "Assemblies (*.dll;*.exe)|*.dll;*.exe|All files (*.*)|*.*",
                FilterIndex = 1
            };
            if (_assemblyPath != null)
                dlg.InitialDirectory = Path.GetDirectoryName(_assemblyPath);
            if (dlg.ShowDialog() == DialogResult.OK)
                LoadAssembly(dlg.FileName);
        }

        private void FormMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length == 1)
                {
                    var ext = Path.GetExtension(files[0]).ToLowerInvariant();
                    if (ext == ".dll" || ext == ".exe")
                    {
                        e.Effect = DragDropEffects.Copy;
                        return;
                    }
                }
            }
            e.Effect = DragDropEffects.None;
        }

        private void FormMain_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length == 1)
                    LoadAssembly(files[0]);
            }
        }

        private string ResolveReferencePath(Binary binary)
        {
            if (binary.FullPath != null && File.Exists(binary.FullPath))
                return binary.FullPath;

            // Use MetadataLoadContext with the NuGet-aware resolver to find the assembly
            try
            {
                var resolver = AssemblyInformationLoader.CreateAssemblyResolver(_assemblyPath);
                using var mlc = new System.Reflection.MetadataLoadContext(resolver);
                var asm = mlc.LoadFromAssemblyName(new AssemblyName(binary.FullName));
                if (!string.IsNullOrEmpty(asm.Location) && File.Exists(asm.Location))
                    return asm.Location;
            }
            catch { }

            // Fallback: check local directory
            var searchDir = Path.GetDirectoryName(_assemblyPath);
            var name = new AssemblyName(binary.FullName).Name;
            var dllPath = Path.Combine(searchDir, name + ".dll");
            if (File.Exists(dllPath)) return dllPath;
            var exePath = Path.Combine(searchDir, name + ".exe");
            if (File.Exists(exePath)) return exePath;

            return null;
        }

        private void UpdateThemeCheckmarks()
        {
            var current = ThemeSettings.Load();
            themeSystemToolStripMenuItem.Checked = current == SystemColorMode.System;
            themeLightToolStripMenuItem.Checked = current == SystemColorMode.Classic;
            themeDarkToolStripMenuItem.Checked = current == SystemColorMode.Dark;
        }

        private void themeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem item && item.Tag is SystemColorMode mode)
            {
                ThemeSettings.Save(mode);
                UpdateThemeCheckmarks();
                MessageBox.Show("Theme will change on next launch.", Resource.AppName,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }

    internal class AssemblyNameComparer : IComparer<AssemblyName>
    {
        public int Compare(AssemblyName x, AssemblyName y)
        {
            return String.Compare(x.FullName, y.FullName, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
