using System;
using System.IO;
using System.Windows.Forms;

namespace AssemblyInformation
{
    internal static class ThemeSettings
    {
        private static readonly string SettingsPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "AssemblyInformation",
            "theme.txt");

        public static SystemColorMode Load()
        {
            try
            {
                if (File.Exists(SettingsPath))
                {
                    var text = File.ReadAllText(SettingsPath).Trim();
                    if (Enum.TryParse<SystemColorMode>(text, out var mode))
                        return mode;
                }
            }
            catch
            {
                // Fall through to default
            }
            return SystemColorMode.Dark;
        }

        public static void Save(SystemColorMode mode)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(SettingsPath));
                File.WriteAllText(SettingsPath, mode.ToString());
            }
            catch
            {
                // Best effort
            }
        }
    }
}
