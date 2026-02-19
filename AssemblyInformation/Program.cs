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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.SetColorMode(SystemColorMode.Dark);
            Application.ThreadException += ApplicationThreadException;

            string assemblyFullPath = null;

            if (args.Length == 1)
            {
                assemblyFullPath = Path.GetFullPath(args[0]);

                if (!File.Exists(assemblyFullPath))
                {
                    MessageBox.Show(string.Format(Resource.FailedToLocateFile, assemblyFullPath), Resource.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            try
            {
                Application.Run(new FormMain(assemblyFullPath));
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Resource.LoadError, ex.Message), Resource.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void ApplicationThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show(string.Format(Resource.LoadError, e.Exception.Message), Resource.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
