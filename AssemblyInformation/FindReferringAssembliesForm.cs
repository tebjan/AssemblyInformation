using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using AssemblyInformation.Model;

namespace AssemblyInformation
{
    public partial class FindReferringAssembliesForm : Form
    {
        private bool cancel;

        public FindReferringAssembliesForm()
        {
            InitializeComponent();
            messageLabel.Text = "";
        }

        public string TestAssemblyFullName { get; set; }

        public string DirectoryPath { get; set; }

        public bool Recursive { get; set; }

        public IEnumerable<string> ReferringAssemblies { get; set; }

        private void FindReferringAssembliesForm_Load(object sender, EventArgs e)
        {
            (new Thread(FindThread)).Start();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            cancel = true;
            cancelButton.Enabled = false;
        }

        private void FindThread()
        {
            DependencyWalker dw = null;
            try
            {
                dw = new DependencyWalker();
                dw.ReferringAssemblyStatusChanged += UpdateStatus;
                ReferringAssemblies = dw.FindReferringAssemblies(TestAssemblyFullName, DirectoryPath, Recursive);
                UpdateStatus(this, new ReferringAssemblyStatusChangeEventArgs { StatusText = "", Progress = -3 });
            }
            catch (Exception)
            {
                UpdateStatus(this, new ReferringAssemblyStatusChangeEventArgs { StatusText = Resource.FailedToListBinaries, Progress = -2 });
            }
            finally
            {
                if (null != dw)
                {
                    dw.ReferringAssemblyStatusChanged -= UpdateStatus;
                }
            }
        }

        private void UpdateStatus(object sender, ReferringAssemblyStatusChangeEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<ReferringAssemblyStatusChangeEventArgs>(UpdateStatus), sender, e);
                return;
            }

            messageLabel.Text = e.StatusText;

            if (e.Progress >= 0)
            {
                progressBar1.Value = e.Progress;
            }

            if (e.Progress == -1)
            {
            }
            else if (e.Progress == -2)
            {
                DialogResult = DialogResult.Cancel;
                MessageBox.Show(e.StatusText, Resource.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }
            else if (e.Progress == -3)
            {
                DialogResult = DialogResult.OK;
                Close();
                return;
            }

            e.Cancel = cancel;
        }
    }
}
