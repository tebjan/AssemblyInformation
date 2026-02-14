using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace AssemblyInformation
{
    internal partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
            Text = $"About {AssemblyTitle}";
            labelProductName.Text = AssemblyProduct;
            labelVersion.Text = $"Version {AssemblyVersion}";
            labelCopyright.Text = AssemblyCopyright;
            textBoxDescription.Text = AssemblyDescription
                + Environment.NewLine + Environment.NewLine
                + "Developed by Tebjan Halm"
                + Environment.NewLine
                + "Originally created by Ashutosh Bhawasinka";

            // Replace company label with clickable website link
            var websiteLink = new LinkLabel
            {
                Text = "https://tebjan.de",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(3, 0, 0, 0)
            };
            websiteLink.LinkClicked += (s, e) =>
            {
                Process.Start(new ProcessStartInfo("https://tebjan.de") { UseShellExecute = true });
            };
            tableLayoutPanel.Controls.Remove(labelCompanyName);
            tableLayoutPanel.Controls.Add(websiteLink, 1, 3);
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    var titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != string.Empty)
                    {
                        return titleAttribute.Title;
                    }
                }

                return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location);
            }
        }

        public string AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public string AssemblyDescription
        {
            get
            {
                return GetAssemblyInformation<AssemblyDescriptionAttribute>(attr => attr.Description);
            }
        }

        public string AssemblyProduct
        {
            get
            {
                return GetAssemblyInformation<AssemblyProductAttribute>(attr => attr.Product);
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                return GetAssemblyInformation<AssemblyCopyrightAttribute>(attr => attr.Copyright);
            }
        }

        public string AssemblyCompany
        {
            get
            {
                return GetAssemblyInformation<AssemblyCompanyAttribute>(attr => attr.Company);
            }
        }

        private string GetAssemblyInformation<T>(Func<T, string> valueSelectorFunc) where T : Attribute
        {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(T), false);
            if (attributes.Length == 0)
            {
                return "";
            }

            return valueSelectorFunc((T)attributes[0]);
        }
        #endregion
    }
}
