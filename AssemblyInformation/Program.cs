using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace AssemblyInformation
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.ThreadException += ApplicationThreadException;

            if (args.Length == 1)
            {
                string filePath = args[0];
                string assemblyFullPath = Path.GetFullPath(filePath);

                if (!File.Exists(assemblyFullPath))
                {
                    MessageBox.Show(string.Format(Resource.FailedToLocateFile, assemblyFullPath), Resource.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new FormMain(assemblyFullPath));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(Resource.LoadError, ex.Message), Resource.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(Resource.UsageString, Resource.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private static void ApplicationThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show(string.Format(Resource.LoadError, e.Exception.Message), Resource.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
