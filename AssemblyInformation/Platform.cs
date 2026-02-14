using System.IO;
using System.Reflection.PortableExecutable;

namespace AssemblyInformation
{
    internal static class Platform
    {
        /// <summary>
        /// Checks if the file at the given path is a .NET assembly.
        /// </summary>
        public static bool IsDotNetAssembly(string filePath)
        {
            try
            {
                using var stream = File.OpenRead(filePath);
                using var peReader = new PEReader(stream);
                return peReader.HasMetadata;
            }
            catch
            {
                return false;
            }
        }
    }
}
