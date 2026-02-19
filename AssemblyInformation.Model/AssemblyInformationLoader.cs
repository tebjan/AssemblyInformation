using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using NuGet.Configuration;

namespace AssemblyInformation.Model
{
    public class AssemblyInformationLoader : IAssemblyInformationLoader
    {
        public static readonly List<string> SystemAssemblies = new List<string>()
        {
            "System",
            "mscorlib",
            "Windows",
            "PresentationCore",
            "PresentationFramework",
            "Microsoft.VisualC",
            "System.Runtime",
            "System.Private",
            "netstandard"
        };

        private static readonly Dictionary<Machine, string> MachineNames = new Dictionary<Machine, string>
        {
            [Machine.I386] = "Targets a 32-bit Intel processor.",
            [Machine.IA64] = "Targets a 64-bit Intel processor.",
            [Machine.Amd64] = "Targets a 64-bit AMD processor.",
            [Machine.Arm] = "Targets an ARM processor.",
            [Machine.Arm64] = "Targets an ARM64 processor.",
        };

        public AssemblyInformationLoader(string filePath)
        {
            FilePath = filePath;
            LoadInformation();
        }

        public string FilePath { get; }

        public string AssemblyFullName { get; private set; }

        public string AssemblyKind { get; private set; }

        public DebuggableAttribute.DebuggingModes? DebuggingFlags { get; private set; }

        public bool EditAndContinueEnabled { get; private set; }

        public string FrameworkVersion { get; private set; }

        public bool IgnoreSymbolStoreSequencePoints { get; private set; }

        public bool JitOptimized { get; private set; }

        /// <summary>
        /// True if in Debugging mode, false if not.
        /// </summary>
        public bool JitTrackingEnabled { get; private set; }

        public string TargetProcessor { get; private set; }

        /// <summary>
        /// True if the file is a .NET managed assembly (has CLI metadata).
        /// </summary>
        public bool IsManaged { get; private set; }

        /// <summary>
        /// If the file is a .NET apphost (native exe with a companion managed dll),
        /// contains the path to the managed assembly. Null otherwise.
        /// </summary>
        public string ApphostManagedDll { get; private set; }

        /// <summary>
        /// Win32 version resource information (available for all PE files).
        /// </summary>
        public FileVersionInfo VersionInfo { get; private set; }

        public AssemblyName[] GetReferencedAssemblies()
        {
            if (!IsManaged) return Array.Empty<AssemblyName>();

            try
            {
                var resolver = CreateAssemblyResolver(FilePath);
                using var mlc = new MetadataLoadContext(resolver);
                var assembly = mlc.LoadFromAssemblyPath(FilePath);
                return assembly.GetReferencedAssemblies();
            }
            catch (Exception)
            {
                return Array.Empty<AssemblyName>();
            }
        }

        private void LoadInformation()
        {
            // Read Win32 version resources (available for all PE files)
            try { VersionInfo = FileVersionInfo.GetVersionInfo(FilePath); } catch { }

            DetermineExecutableKind();
            if (IsManaged)
            {
                DetermineMetadata();
            }
            else
            {
                // For native files, build a rich display name from version resources
                AssemblyFullName = BuildNativeDisplayName();
                FrameworkVersion ??= "N/A (native)";
            }
        }

        private string BuildNativeDisplayName()
        {
            if (VersionInfo == null)
                return Path.GetFileName(FilePath);

            var parts = new List<string>();

            if (!string.IsNullOrEmpty(VersionInfo.FileDescription))
                parts.Add(VersionInfo.FileDescription);
            else
                parts.Add(Path.GetFileName(FilePath));

            if (!string.IsNullOrEmpty(VersionInfo.FileVersion))
                parts.Add($"Version: {VersionInfo.FileVersion}");
            if (!string.IsNullOrEmpty(VersionInfo.CompanyName))
                parts.Add(VersionInfo.CompanyName);
            if (!string.IsNullOrEmpty(VersionInfo.LegalCopyright))
                parts.Add(VersionInfo.LegalCopyright);

            return parts.Count > 0 ? string.Join("\r\n", parts) : Path.GetFileName(FilePath);
        }

        private void DetermineExecutableKind()
        {
            using var stream = File.OpenRead(FilePath);
            using var peReader = new PEReader(stream);

            var peHeaders = peReader.PEHeaders;
            var machine = peHeaders.CoffHeader.Machine;

            // Determine target processor from PE header (works for native and managed)
            if (MachineNames.TryGetValue(machine, out var machineName))
            {
                TargetProcessor = machineName;
            }
            else
            {
                TargetProcessor = $"Unknown ({machine})";
            }

            if (!peReader.HasMetadata)
            {
                IsManaged = false;

                // Check if this is a .NET single-file bundle
                if (IsSingleFileBundle(FilePath, out var fileCount))
                {
                    AssemblyKind = $"Single-file .NET bundle ({fileCount} embedded files)";
                    AssemblyFullName = Path.GetFileName(FilePath);
                    FrameworkVersion = "Embedded in bundle";
                    return;
                }

                // Check if this is a .NET apphost (native exe with companion managed dll)
                var ext = Path.GetExtension(FilePath);
                if (string.Equals(ext, ".exe", StringComparison.OrdinalIgnoreCase))
                {
                    var companionDll = Path.ChangeExtension(FilePath, ".dll");
                    if (File.Exists(companionDll))
                    {
                        try
                        {
                            using var dllStream = File.OpenRead(companionDll);
                            using var dllPe = new PEReader(dllStream);
                            if (dllPe.HasMetadata)
                            {
                                ApphostManagedDll = companionDll;
                                AssemblyKind = $".NET runtime host (managed assembly is {Path.GetFileName(companionDll)})";
                                return;
                            }
                        }
                        catch { }
                    }
                }

                AssemblyKind = "Native binary (not a .NET assembly)";
                return;
            }

            IsManaged = true;
            var corFlags = peHeaders.CorHeader?.Flags ?? 0;

            // Determine assembly kind from CorFlags
            var kindParts = new List<string>();
            if ((corFlags & CorFlags.ILOnly) != 0)
                kindParts.Add("- Contains only Microsoft intermediate language (MSIL), and is therefore neutral with respect to 32-bit or 64-bit platforms.");
            if (peHeaders.PEHeader.Magic == PEMagic.PE32Plus)
                kindParts.Add("- Requires a 64-bit platform.");
            if ((corFlags & CorFlags.Requires32Bit) != 0)
                kindParts.Add("- Can be run on a 32-bit platform, or in the 32-bit Windows on Windows (WOW) environment on a 64-bit platform.");
            if ((corFlags & CorFlags.Prefers32Bit) != 0)
                kindParts.Add("- Platform-agnostic, but 32-bit preferred.");

            AssemblyKind = kindParts.Count > 0 ? string.Join(Environment.NewLine, kindParts) : "Unknown";

            // Any CPU: ILOnly + I386 without Requires32Bit
            if (machine == Machine.I386 &&
                (corFlags & CorFlags.ILOnly) != 0 &&
                (corFlags & CorFlags.Requires32Bit) == 0 &&
                (corFlags & CorFlags.Prefers32Bit) == 0)
            {
                TargetProcessor = "AnyCPU";
            }
        }

        private void DetermineMetadata()
        {
            var resolver = CreateAssemblyResolver(FilePath);
            using var mlc = new MetadataLoadContext(resolver);

            Assembly assembly;
            try
            {
                assembly = mlc.LoadFromAssemblyPath(FilePath);
            }
            catch (Exception)
            {
                AssemblyFullName = Path.GetFileName(FilePath);
                FrameworkVersion = "Unknown";
                return;
            }

            AssemblyFullName = assembly.FullName;
            FrameworkVersion = assembly.ImageRuntimeVersion;

            try
            {
                DetermineDebuggingAttributes(assembly);
                DetermineFrameworkVersion(assembly);
            }
            catch (Exception)
            {
                // Some assemblies reference types that can't be resolved
                // (e.g. NuGet packages with dependencies in other directories).
                // Show what we have rather than crashing.
            }
        }

        private void DetermineDebuggingAttributes(Assembly assembly)
        {
            // MetadataLoadContext can't instantiate attributes, so use GetCustomAttributesData
            var debugAttrData = assembly.GetCustomAttributesData()
                .FirstOrDefault(a => a.AttributeType.FullName == "System.Diagnostics.DebuggableAttribute");

            if (debugAttrData != null)
            {
                // DebuggableAttribute has two constructors:
                // (DebuggingModes) or (bool isJITTrackingEnabled, bool isJITOptimizerDisabled)
                if (debugAttrData.ConstructorArguments.Count == 1)
                {
                    // Single arg: DebuggingModes enum value
                    var flags = (DebuggableAttribute.DebuggingModes)(int)debugAttrData.ConstructorArguments[0].Value;
                    DebuggingFlags = flags;
                    JitTrackingEnabled = (flags & DebuggableAttribute.DebuggingModes.Default) != 0;
                    JitOptimized = (flags & DebuggableAttribute.DebuggingModes.DisableOptimizations) == 0;
                    IgnoreSymbolStoreSequencePoints = (flags & DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints) != 0;
                    EditAndContinueEnabled = (flags & DebuggableAttribute.DebuggingModes.EnableEditAndContinue) != 0;
                }
                else if (debugAttrData.ConstructorArguments.Count == 2)
                {
                    // Two args: (bool isJITTrackingEnabled, bool isJITOptimizerDisabled)
                    JitTrackingEnabled = (bool)debugAttrData.ConstructorArguments[0].Value;
                    JitOptimized = !(bool)debugAttrData.ConstructorArguments[1].Value;
                    IgnoreSymbolStoreSequencePoints = false;
                    EditAndContinueEnabled = false;
                }
            }
            else
            {
                JitTrackingEnabled = false;
                JitOptimized = true;
                IgnoreSymbolStoreSequencePoints = false;
                EditAndContinueEnabled = false;
                DebuggingFlags = null;
            }
        }

        private void DetermineFrameworkVersion(Assembly assembly)
        {
            var targetFwAttr = assembly.GetCustomAttributesData()
                .FirstOrDefault(a => a.AttributeType.FullName == "System.Runtime.Versioning.TargetFrameworkAttribute");

            if (targetFwAttr != null)
            {
                // First constructor arg is the FrameworkName string (e.g., ".NETFramework,Version=v4.7")
                var frameworkName = targetFwAttr.ConstructorArguments[0].Value?.ToString();

                // Check for FrameworkDisplayName named argument
                var displayNameArg = targetFwAttr.NamedArguments?
                    .FirstOrDefault(a => a.MemberName == "FrameworkDisplayName");
                if (displayNameArg.HasValue && displayNameArg.Value.TypedValue.Value is string displayName && !string.IsNullOrEmpty(displayName))
                {
                    FrameworkVersion = displayName;
                }
                else if (!string.IsNullOrEmpty(frameworkName))
                {
                    FrameworkVersion = frameworkName;
                }
            }
        }

        /// <summary>
        /// Detects whether a file is a .NET single-file bundle by checking for the bundle signature.
        /// </summary>
        private static bool IsSingleFileBundle(string filePath, out int fileCount)
        {
            fileCount = 0;
            try
            {
                // .NET single-file bundle signature: SHA-256 of ".net core bundle"
                byte[] bundleSignature =
                {
                    0x8b, 0x12, 0x02, 0xb9, 0x6a, 0x61, 0x20, 0x38,
                    0x72, 0x7b, 0x4d, 0x12, 0xa1, 0x78, 0xc5, 0x7b,
                    0x22, 0x57, 0xee, 0xf6, 0x57, 0x10, 0x69, 0xe4,
                    0x20, 0x52, 0x2c, 0x8f, 0x43, 0x75, 0x65, 0xf9
                };

                using var stream = File.OpenRead(filePath);
                if (stream.Length < 50) return false;

                // Last 8 bytes of the file contain the offset to the bundle header
                stream.Seek(-8, SeekOrigin.End);
                var offsetBuffer = new byte[8];
                stream.Read(offsetBuffer, 0, 8);
                long headerOffset = BitConverter.ToInt64(offsetBuffer, 0);

                if (headerOffset <= 0 || headerOffset >= stream.Length - 32) return false;

                // Read signature at the header offset
                stream.Seek(headerOffset, SeekOrigin.Begin);
                var signature = new byte[32];
                stream.Read(signature, 0, 32);

                for (int i = 0; i < 32; i++)
                {
                    if (signature[i] != bundleSignature[i]) return false;
                }

                // Read version (major, minor) and file count
                var header = new byte[12];
                stream.Read(header, 0, 12);
                fileCount = BitConverter.ToInt32(header, 8);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static PathAssemblyResolver CreateAssemblyResolver(string assemblyPath)
        {
            var assemblyDir = Path.GetDirectoryName(assemblyPath);

            // Keyed by file name (e.g. "mscorlib.dll") to prevent duplicates.
            // Earlier additions win, so order matters: target dir > runtime > framework refs.
            var pathsByName = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            void AddFile(string file)
            {
                var name = Path.GetFileName(file);
                pathsByName.TryAdd(name, file);
            }

            // 1. Target assembly's directory (highest priority)
            if (!string.IsNullOrEmpty(assemblyDir))
            {
                foreach (var file in Directory.GetFiles(assemblyDir, "*.dll"))
                    AddFile(file);
                foreach (var file in Directory.GetFiles(assemblyDir, "*.exe"))
                    AddFile(file);
            }

            // 2. Current runtime's assemblies
            var runtimeDir = Path.GetDirectoryName(typeof(object).Assembly.Location);
            if (!string.IsNullOrEmpty(runtimeDir))
            {
                foreach (var file in Directory.GetFiles(runtimeDir, "*.dll"))
                    AddFile(file);
            }

            // 3. NuGet package caches — scan sibling packages and global cache
            AddNuGetPackageAssemblies(assemblyDir, AddFile);

            // 4. .NET Framework reference assemblies (for inspecting Framework assemblies)
            var frameworkRefBase = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
                "Reference Assemblies", "Microsoft", "Framework", ".NETFramework");

            if (Directory.Exists(frameworkRefBase))
            {
                var versions = Directory.GetDirectories(frameworkRefBase)
                    .OrderByDescending(d => d)
                    .ToList();

                foreach (var versionDir in versions)
                {
                    foreach (var file in Directory.GetFiles(versionDir, "*.dll"))
                        AddFile(file);

                    var facadesDir = Path.Combine(versionDir, "Facades");
                    if (Directory.Exists(facadesDir))
                    {
                        foreach (var file in Directory.GetFiles(facadesDir, "*.dll"))
                            AddFile(file);
                    }
                }
            }

            // Ensure the target assembly itself is included
            pathsByName[Path.GetFileName(assemblyPath)] = assemblyPath;

            return new PathAssemblyResolver(pathsByName.Values);
        }

        /// <summary>
        /// Scans NuGet package directories to resolve assembly dependencies.
        /// Checks: sibling packages when inside a NuGet-like directory, and all NuGet global/fallback folders.
        /// </summary>
        private static void AddNuGetPackageAssemblies(string assemblyDir, Action<string> addFile)
        {
            if (string.IsNullOrEmpty(assemblyDir)) return;

            // Detect if the assembly is inside a NuGet package layout:
            // .../packages/PackageName.Version/lib/tfm/Assembly.dll
            var tfmDir = assemblyDir;
            var libDir = Path.GetDirectoryName(tfmDir);
            if (libDir != null && Path.GetFileName(libDir).Equals("lib", StringComparison.OrdinalIgnoreCase))
            {
                var packageDir = Path.GetDirectoryName(libDir);
                var packagesRoot = packageDir != null ? Path.GetDirectoryName(packageDir) : null;
                if (packagesRoot != null)
                {
                    var tfmName = Path.GetFileName(tfmDir);
                    ScanNuGetRoot(packagesRoot, tfmName, addFile);
                }
            }

            // Use official NuGet API to find all package folders
            var tfmHint = Path.GetFileName(tfmDir);
            try
            {
                var settings = Settings.LoadDefaultSettings(assemblyDir);
                var globalFolder = SettingsUtility.GetGlobalPackagesFolder(settings);
                if (Directory.Exists(globalFolder))
                    ScanNuGetGlobalCache(globalFolder, tfmHint, addFile);

                foreach (var fallback in SettingsUtility.GetFallbackPackageFolders(settings))
                {
                    if (Directory.Exists(fallback))
                        ScanNuGetRoot(fallback, tfmHint, addFile);
                }
            }
            catch
            {
                // NuGet configuration might not be available — best effort
            }
        }

        /// <summary>
        /// Scans the global NuGet cache which has a two-level structure: package-name/version/lib/tfm/.
        /// </summary>
        private static void ScanNuGetGlobalCache(string globalCache, string preferredTfm, Action<string> addFile)
        {
            if (!Directory.Exists(globalCache)) return;

            var tfmBase = preferredTfm;
            var dashIdx = preferredTfm.IndexOf('-');
            if (dashIdx > 0) tfmBase = preferredTfm.Substring(0, dashIdx);

            try
            {
                foreach (var packageNameDir in Directory.GetDirectories(globalCache))
                {
                    // Pick the latest version directory
                    var versionDirs = Directory.GetDirectories(packageNameDir);
                    if (versionDirs.Length == 0) continue;
                    var latestVersion = versionDirs.OrderByDescending(d => Path.GetFileName(d)).First();

                    var libDir = Path.Combine(latestVersion, "lib");
                    if (!Directory.Exists(libDir)) continue;

                    var tfmDir = FindBestTfmDirectory(libDir, preferredTfm, tfmBase);
                    if (tfmDir != null)
                    {
                        foreach (var file in Directory.GetFiles(tfmDir, "*.dll"))
                            addFile(file);
                    }
                }
            }
            catch
            {
                // Best effort
            }
        }

        private static void ScanNuGetRoot(string packagesRoot, string preferredTfm, Action<string> addFile)
        {
            if (!Directory.Exists(packagesRoot)) return;

            // Extract the base TFM prefix for matching (e.g. "net8.0-windows7.0" -> "net8.0")
            var tfmBase = preferredTfm;
            var dashIdx = preferredTfm.IndexOf('-');
            if (dashIdx > 0) tfmBase = preferredTfm.Substring(0, dashIdx);

            try
            {
                foreach (var packageDir in Directory.GetDirectories(packagesRoot))
                {
                    var libDir = Path.Combine(packageDir, "lib");
                    if (!Directory.Exists(libDir)) continue;

                    // Try to find the best matching TFM directory
                    var tfmDir = FindBestTfmDirectory(libDir, preferredTfm, tfmBase);
                    if (tfmDir != null)
                    {
                        foreach (var file in Directory.GetFiles(tfmDir, "*.dll"))
                            addFile(file);
                    }
                }
            }
            catch
            {
                // Access errors, etc. — best effort
            }
        }

        private static string FindBestTfmDirectory(string libDir, string preferredTfm, string tfmBase)
        {
            string[] tfmDirs;
            try { tfmDirs = Directory.GetDirectories(libDir); }
            catch { return null; }

            // Priority: exact match > base prefix match > netstandard (highest version)
            string exactMatch = null, prefixMatch = null, netstandardMatch = null;
            foreach (var dir in tfmDirs)
            {
                var name = Path.GetFileName(dir);
                if (name.Equals(preferredTfm, StringComparison.OrdinalIgnoreCase))
                {
                    exactMatch = dir;
                    break;
                }
                if (prefixMatch == null && name.StartsWith(tfmBase, StringComparison.OrdinalIgnoreCase))
                    prefixMatch = dir;
                if (name.StartsWith("netstandard", StringComparison.OrdinalIgnoreCase))
                {
                    if (netstandardMatch == null ||
                        string.Compare(name, Path.GetFileName(netstandardMatch), StringComparison.OrdinalIgnoreCase) > 0)
                        netstandardMatch = dir;
                }
            }

            return exactMatch ?? prefixMatch ?? netstandardMatch;
        }
    }
}
