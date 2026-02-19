namespace AssemblyInformation
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            lblCompilation = new System.Windows.Forms.Label();
            lblFullName = new System.Windows.Forms.Label();
            panel1 = new System.Windows.Forms.Panel();
            frameWorkVersion = new System.Windows.Forms.TextBox();
            tabControl1 = new System.Windows.Forms.TabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            dependencyTreeView = new System.Windows.Forms.TreeView();
            tabPage2 = new System.Windows.Forms.TabPage();
            referenceListListBox = new System.Windows.Forms.ListBox();
            tabPage3 = new System.Windows.Forms.TabPage();
            referringAssembliesListtBox = new System.Windows.Forms.ListBox();
            referringAssemblyBrowseFolderButton = new System.Windows.Forms.Button();
            referringAssemblyFolderSearchButton = new System.Windows.Forms.Button();
            referringAssemblyFolderTextBox = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            tabPage4 = new System.Windows.Forms.TabPage();
            versionInfoListView = new System.Windows.Forms.ListView();
            columnHeaderProperty = new System.Windows.Forms.ColumnHeader();
            columnHeaderValue = new System.Windows.Forms.ColumnHeader();
            targetProcessorTextBox = new System.Windows.Forms.TextBox();
            assemblyKindTextBox = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            lblReferences = new System.Windows.Forms.Label();
            txtEditAndContinue = new System.Windows.Forms.TextBox();
            txtSequencing = new System.Windows.Forms.TextBox();
            txtOptimized = new System.Windows.Forms.TextBox();
            txtTrackingEnabled = new System.Windows.Forms.TextBox();
            txtFullName = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            openAssemblyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            hideGACAssembliesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            showAssemblyFullNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            DebuggableFlagsToolTip = new System.Windows.Forms.ToolTip(components);
            panel1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            tabPage4.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // lblCompilation
            // 
            lblCompilation.AutoSize = true;
            lblCompilation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 177);
            lblCompilation.Location = new System.Drawing.Point(12, 43);
            lblCompilation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblCompilation.Name = "lblCompilation";
            lblCompilation.Size = new System.Drawing.Size(76, 13);
            lblCompilation.TabIndex = 0;
            lblCompilation.Text = "Compilation:";
            lblCompilation.MouseEnter += LblCompilationMouseEnter;
            lblCompilation.MouseLeave += LblCompilationMouseLeave;
            // 
            // lblFullName
            // 
            lblFullName.AutoSize = true;
            lblFullName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 177);
            lblFullName.Location = new System.Drawing.Point(12, 145);
            lblFullName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new System.Drawing.Size(67, 13);
            lblFullName.TabIndex = 9;
            lblFullName.Text = "Full Name:";
            // 
            // panel1
            // 
            panel1.Controls.Add(frameWorkVersion);
            panel1.Controls.Add(tabControl1);
            panel1.Controls.Add(targetProcessorTextBox);
            panel1.Controls.Add(assemblyKindTextBox);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(lblReferences);
            panel1.Controls.Add(txtEditAndContinue);
            panel1.Controls.Add(txtSequencing);
            panel1.Controls.Add(txtOptimized);
            panel1.Controls.Add(txtTrackingEnabled);
            panel1.Controls.Add(txtFullName);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(lblCompilation);
            panel1.Controls.Add(lblFullName);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(952, 471);
            panel1.TabIndex = 2;
            // 
            // frameWorkVersion
            // 
            frameWorkVersion.BackColor = System.Drawing.SystemColors.Control;
            frameWorkVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            frameWorkVersion.Location = new System.Drawing.Point(692, 39);
            frameWorkVersion.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            frameWorkVersion.Name = "frameWorkVersion";
            frameWorkVersion.ReadOnly = true;
            frameWorkVersion.Size = new System.Drawing.Size(116, 23);
            frameWorkVersion.TabIndex = 13;
            // 
            // tabControl1
            // 
            tabControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Location = new System.Drawing.Point(153, 197);
            tabControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabControl1.Multiline = true;
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(785, 273);
            tabControl1.TabIndex = 12;
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = System.Drawing.SystemColors.Control;
            tabPage1.Controls.Add(dependencyTreeView);
            tabPage1.Location = new System.Drawing.Point(4, 24);
            tabPage1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage1.Size = new System.Drawing.Size(777, 245);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Direct References";
            // 
            // dependencyTreeView
            // 
            dependencyTreeView.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dependencyTreeView.BackColor = System.Drawing.SystemColors.Control;
            dependencyTreeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            dependencyTreeView.Location = new System.Drawing.Point(-4, 0);
            dependencyTreeView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            dependencyTreeView.Name = "dependencyTreeView";
            dependencyTreeView.Size = new System.Drawing.Size(780, 247);
            dependencyTreeView.TabIndex = 11;
            dependencyTreeView.BeforeExpand += DependencyTreeViewBeforeExpand;
            dependencyTreeView.MouseDoubleClick += DependencyTreeViewMouseDoubleClick;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = System.Drawing.SystemColors.Control;
            tabPage2.Controls.Add(referenceListListBox);
            tabPage2.Location = new System.Drawing.Point(4, 24);
            tabPage2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage2.Size = new System.Drawing.Size(777, 245);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "All Direct & Indirect References";
            // 
            // referenceListListBox
            // 
            referenceListListBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            referenceListListBox.BackColor = System.Drawing.SystemColors.Control;
            referenceListListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            referenceListListBox.FormattingEnabled = true;
            referenceListListBox.Location = new System.Drawing.Point(0, 0);
            referenceListListBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            referenceListListBox.Name = "referenceListListBox";
            referenceListListBox.Size = new System.Drawing.Size(773, 242);
            referenceListListBox.TabIndex = 0;
            referenceListListBox.MouseDoubleClick += AssemblyListBoxMouseDoubleClick;
            // 
            // tabPage3
            // 
            tabPage3.BackColor = System.Drawing.SystemColors.Control;
            tabPage3.Controls.Add(referringAssembliesListtBox);
            tabPage3.Controls.Add(referringAssemblyBrowseFolderButton);
            tabPage3.Controls.Add(referringAssemblyFolderSearchButton);
            tabPage3.Controls.Add(referringAssemblyFolderTextBox);
            tabPage3.Controls.Add(label3);
            tabPage3.Location = new System.Drawing.Point(4, 24);
            tabPage3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage3.Size = new System.Drawing.Size(777, 245);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Referring Assemblies";
            // 
            // referringAssembliesListtBox
            // 
            referringAssembliesListtBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            referringAssembliesListtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            referringAssembliesListtBox.FormattingEnabled = true;
            referringAssembliesListtBox.Location = new System.Drawing.Point(8, 54);
            referringAssembliesListtBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            referringAssembliesListtBox.Name = "referringAssembliesListtBox";
            referringAssembliesListtBox.Size = new System.Drawing.Size(765, 182);
            referringAssembliesListtBox.TabIndex = 13;
            referringAssembliesListtBox.MouseDoubleClick += AssemblyListBoxMouseDoubleClick;
            // 
            // referringAssemblyBrowseFolderButton
            // 
            referringAssemblyBrowseFolderButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            referringAssemblyBrowseFolderButton.Location = new System.Drawing.Point(666, 24);
            referringAssemblyBrowseFolderButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            referringAssemblyBrowseFolderButton.Name = "referringAssemblyBrowseFolderButton";
            referringAssemblyBrowseFolderButton.Size = new System.Drawing.Size(29, 27);
            referringAssemblyBrowseFolderButton.TabIndex = 2;
            referringAssemblyBrowseFolderButton.Text = "...";
            referringAssemblyBrowseFolderButton.UseVisualStyleBackColor = true;
            referringAssemblyBrowseFolderButton.Click += referringAssemblyBrowseFolderButton_Click;
            // 
            // referringAssemblyFolderSearchButton
            // 
            referringAssemblyFolderSearchButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            referringAssemblyFolderSearchButton.Location = new System.Drawing.Point(702, 24);
            referringAssemblyFolderSearchButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            referringAssemblyFolderSearchButton.Name = "referringAssemblyFolderSearchButton";
            referringAssemblyFolderSearchButton.Size = new System.Drawing.Size(66, 27);
            referringAssemblyFolderSearchButton.TabIndex = 3;
            referringAssemblyFolderSearchButton.Text = "Find";
            referringAssemblyFolderSearchButton.UseVisualStyleBackColor = true;
            referringAssemblyFolderSearchButton.Click += referringAssemblyFolderSearchButton_Click;
            // 
            // referringAssemblyFolderTextBox
            // 
            referringAssemblyFolderTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            referringAssemblyFolderTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            referringAssemblyFolderTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            referringAssemblyFolderTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            referringAssemblyFolderTextBox.Location = new System.Drawing.Point(8, 24);
            referringAssemblyFolderTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            referringAssemblyFolderTextBox.Name = "referringAssemblyFolderTextBox";
            referringAssemblyFolderTextBox.Size = new System.Drawing.Size(650, 23);
            referringAssemblyFolderTextBox.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(5, 5);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(354, 15);
            label3.TabIndex = 0;
            label3.Text = "Select the directory in which you want to find referring assemblies";
            // 
            // tabPage4
            // 
            tabPage4.BackColor = System.Drawing.SystemColors.Control;
            tabPage4.Controls.Add(versionInfoListView);
            tabPage4.Location = new System.Drawing.Point(4, 24);
            tabPage4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage4.Size = new System.Drawing.Size(777, 245);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Version Info";
            // 
            // versionInfoListView
            // 
            versionInfoListView.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            versionInfoListView.BackColor = System.Drawing.SystemColors.Control;
            versionInfoListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            versionInfoListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { columnHeaderProperty, columnHeaderValue });
            versionInfoListView.FullRowSelect = true;
            versionInfoListView.GridLines = true;
            versionInfoListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            versionInfoListView.Location = new System.Drawing.Point(0, 0);
            versionInfoListView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            versionInfoListView.Name = "versionInfoListView";
            versionInfoListView.Size = new System.Drawing.Size(775, 243);
            versionInfoListView.TabIndex = 0;
            versionInfoListView.UseCompatibleStateImageBehavior = false;
            versionInfoListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderProperty
            // 
            columnHeaderProperty.Text = "Property";
            columnHeaderProperty.Width = 180;
            // 
            // columnHeaderValue
            // 
            columnHeaderValue.Text = "Value";
            columnHeaderValue.Width = 460;
            // 
            // targetProcessorTextBox
            // 
            targetProcessorTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            targetProcessorTextBox.BackColor = System.Drawing.SystemColors.Control;
            targetProcessorTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            targetProcessorTextBox.Location = new System.Drawing.Point(153, 121);
            targetProcessorTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            targetProcessorTextBox.Name = "targetProcessorTextBox";
            targetProcessorTextBox.ReadOnly = true;
            targetProcessorTextBox.Size = new System.Drawing.Size(784, 23);
            targetProcessorTextBox.TabIndex = 8;
            // 
            // assemblyKindTextBox
            // 
            assemblyKindTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            assemblyKindTextBox.BackColor = System.Drawing.SystemColors.Control;
            assemblyKindTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            assemblyKindTextBox.Location = new System.Drawing.Point(153, 73);
            assemblyKindTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            assemblyKindTextBox.Multiline = true;
            assemblyKindTextBox.Name = "assemblyKindTextBox";
            assemblyKindTextBox.ReadOnly = true;
            assemblyKindTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            assemblyKindTextBox.Size = new System.Drawing.Size(785, 43);
            assemblyKindTextBox.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label2.Location = new System.Drawing.Point(12, 115);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(104, 13);
            label2.TabIndex = 7;
            label2.Text = "Target Processor";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label1.Location = new System.Drawing.Point(12, 73);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(88, 13);
            label1.TabIndex = 5;
            label1.Text = "Assembly Kind";
            // 
            // lblReferences
            // 
            lblReferences.AutoSize = true;
            lblReferences.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 177);
            lblReferences.Location = new System.Drawing.Point(12, 194);
            lblReferences.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblReferences.Name = "lblReferences";
            lblReferences.Size = new System.Drawing.Size(76, 13);
            lblReferences.TabIndex = 11;
            lblReferences.Text = "References:";
            // 
            // txtEditAndContinue
            // 
            txtEditAndContinue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtEditAndContinue.ForeColor = System.Drawing.SystemColors.Info;
            txtEditAndContinue.Location = new System.Drawing.Point(407, 39);
            txtEditAndContinue.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtEditAndContinue.Name = "txtEditAndContinue";
            txtEditAndContinue.ReadOnly = true;
            txtEditAndContinue.Size = new System.Drawing.Size(158, 23);
            txtEditAndContinue.TabIndex = 4;
            txtEditAndContinue.Text = "Edit and Continue Disabled";
            // 
            // txtSequencing
            // 
            txtSequencing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtSequencing.ForeColor = System.Drawing.SystemColors.Info;
            txtSequencing.Location = new System.Drawing.Point(296, 39);
            txtSequencing.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtSequencing.Name = "txtSequencing";
            txtSequencing.ReadOnly = true;
            txtSequencing.Size = new System.Drawing.Size(109, 23);
            txtSequencing.TabIndex = 3;
            txtSequencing.Text = "MSIL Sequencing";
            // 
            // txtOptimized
            // 
            txtOptimized.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtOptimized.ForeColor = System.Drawing.SystemColors.Info;
            txtOptimized.Location = new System.Drawing.Point(209, 39);
            txtOptimized.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtOptimized.Name = "txtOptimized";
            txtOptimized.ReadOnly = true;
            txtOptimized.Size = new System.Drawing.Size(86, 23);
            txtOptimized.TabIndex = 2;
            txtOptimized.Text = "Not Optimized";
            // 
            // txtTrackingEnabled
            // 
            txtTrackingEnabled.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtTrackingEnabled.ForeColor = System.Drawing.SystemColors.Info;
            txtTrackingEnabled.Location = new System.Drawing.Point(153, 39);
            txtTrackingEnabled.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtTrackingEnabled.Name = "txtTrackingEnabled";
            txtTrackingEnabled.ReadOnly = true;
            txtTrackingEnabled.Size = new System.Drawing.Size(54, 23);
            txtTrackingEnabled.TabIndex = 1;
            txtTrackingEnabled.Text = "Release";
            // 
            // txtFullName
            // 
            txtFullName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            txtFullName.BackColor = System.Drawing.SystemColors.Control;
            txtFullName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtFullName.Location = new System.Drawing.Point(153, 148);
            txtFullName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            txtFullName.Multiline = true;
            txtFullName.Name = "txtFullName";
            txtFullName.ReadOnly = true;
            txtFullName.Size = new System.Drawing.Size(784, 41);
            txtFullName.TabIndex = 10;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 177);
            label4.Location = new System.Drawing.Point(573, 43);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(96, 13);
            label4.TabIndex = 0;
            label4.Text = ".Net Framework";
            label4.MouseEnter += LblCompilationMouseEnter;
            label4.MouseLeave += LblCompilationMouseLeave;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { aboutToolStripMenuItem, viewToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            menuStrip1.Size = new System.Drawing.Size(952, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { openAssemblyToolStripMenuItem, toolStripSeparator1, aboutToolStripMenuItem1, exitToolStripMenuItem });
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            aboutToolStripMenuItem.Text = "Options";
            // 
            // openAssemblyToolStripMenuItem
            // 
            openAssemblyToolStripMenuItem.Name = "openAssemblyToolStripMenuItem";
            openAssemblyToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O;
            openAssemblyToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            openAssemblyToolStripMenuItem.Text = "Open Assembly...";
            openAssemblyToolStripMenuItem.Click += OpenAssemblyToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(206, 6);
            // 
            // aboutToolStripMenuItem1
            // 
            aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            aboutToolStripMenuItem1.Size = new System.Drawing.Size(209, 22);
            aboutToolStripMenuItem1.Text = "About";
            aboutToolStripMenuItem1.Click += AboutToolStripMenuItem1Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += ExitToolStripMenuItemClick;
            // 
            // viewToolStripMenuItem
            // 
            themeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            themeSystemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            themeLightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            themeDarkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { hideGACAssembliesToolStripMenuItem, showAssemblyFullNameToolStripMenuItem, toolStripSeparator2, themeToolStripMenuItem });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            viewToolStripMenuItem.Text = "View";
            // 
            // hideGACAssembliesToolStripMenuItem
            // 
            hideGACAssembliesToolStripMenuItem.Name = "hideGACAssembliesToolStripMenuItem";
            hideGACAssembliesToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            hideGACAssembliesToolStripMenuItem.Text = "Hide GAC Assemblies";
            hideGACAssembliesToolStripMenuItem.Click += hideGACAssembliesToolStripMenuItem_Click;
            // 
            // showAssemblyFullNameToolStripMenuItem
            // 
            showAssemblyFullNameToolStripMenuItem.Checked = true;
            showAssemblyFullNameToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            showAssemblyFullNameToolStripMenuItem.Name = "showAssemblyFullNameToolStripMenuItem";
            showAssemblyFullNameToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            showAssemblyFullNameToolStripMenuItem.Text = "Show Assembly Full Name";
            showAssemblyFullNameToolStripMenuItem.Click += showAssemblyFullNameToolStripMenuItem_Click;
            //
            // toolStripSeparator2
            //
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(211, 6);
            //
            // themeToolStripMenuItem
            //
            themeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { themeSystemToolStripMenuItem, themeLightToolStripMenuItem, themeDarkToolStripMenuItem });
            themeToolStripMenuItem.Name = "themeToolStripMenuItem";
            themeToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            themeToolStripMenuItem.Text = "Theme";
            //
            // themeSystemToolStripMenuItem
            //
            themeSystemToolStripMenuItem.Name = "themeSystemToolStripMenuItem";
            themeSystemToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            themeSystemToolStripMenuItem.Text = "System Default";
            themeSystemToolStripMenuItem.Click += themeToolStripMenuItem_Click;
            themeSystemToolStripMenuItem.Tag = System.Windows.Forms.SystemColorMode.System;
            //
            // themeLightToolStripMenuItem
            //
            themeLightToolStripMenuItem.Name = "themeLightToolStripMenuItem";
            themeLightToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            themeLightToolStripMenuItem.Text = "Light";
            themeLightToolStripMenuItem.Click += themeToolStripMenuItem_Click;
            themeLightToolStripMenuItem.Tag = System.Windows.Forms.SystemColorMode.Classic;
            //
            // themeDarkToolStripMenuItem
            //
            themeDarkToolStripMenuItem.Name = "themeDarkToolStripMenuItem";
            themeDarkToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            themeDarkToolStripMenuItem.Text = "Dark";
            themeDarkToolStripMenuItem.Click += themeToolStripMenuItem_Click;
            themeDarkToolStripMenuItem.Tag = System.Windows.Forms.SystemColorMode.Dark;
            //
            // FormMain
            // 
            AllowDrop = true;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(952, 471);
            Controls.Add(menuStrip1);
            Controls.Add(panel1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MinimumSize = new System.Drawing.Size(177, 121);
            Name = "FormMain";
            Text = "Assembly Information";
            Load += FormMainLoad;
            DragDrop += FormMain_DragDrop;
            DragEnter += FormMain_DragEnter;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            tabPage4.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCompilation;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.TextBox txtTrackingEnabled;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label lblReferences;
        private System.Windows.Forms.TextBox txtOptimized;
        private System.Windows.Forms.ToolTip DebuggableFlagsToolTip;
        private System.Windows.Forms.TextBox txtSequencing;
        private System.Windows.Forms.TextBox txtEditAndContinue;
        private System.Windows.Forms.TextBox targetProcessorTextBox;
        private System.Windows.Forms.TextBox assemblyKindTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView dependencyTreeView;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button referringAssemblyFolderSearchButton;
        private System.Windows.Forms.TextBox referringAssemblyFolderTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button referringAssemblyBrowseFolderButton;
        private System.Windows.Forms.ListBox referringAssembliesListtBox;
        private System.Windows.Forms.ListBox referenceListListBox;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideGACAssembliesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showAssemblyFullNameToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox frameWorkVersion;
        private System.Windows.Forms.ToolStripMenuItem openAssemblyToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ListView versionInfoListView;
        private System.Windows.Forms.ColumnHeader columnHeaderProperty;
        private System.Windows.Forms.ColumnHeader columnHeaderValue;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem themeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem themeSystemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem themeLightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem themeDarkToolStripMenuItem;
    }
}