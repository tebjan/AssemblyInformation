using System.Reflection;

namespace AssemblyInformation
{
    public class Binary
    {
        public Binary(AssemblyName assemblyName, string fullPath = null, bool isSystemBinary = false)
        {
            FullName = assemblyName.FullName;
            DisplayName = assemblyName.Name;
            FullPath = fullPath;
            IsSystemBinary = isSystemBinary;
        }

        public string DisplayName { get; private set; }

        public string FullName { get; private set; }

        public string FullPath { get; private set; }

        public bool IsSystemBinary { get; set; }
    }
}
