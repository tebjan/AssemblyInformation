using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AssemblyInformation.Model
{
    public class DependencyWalker
    {
        private readonly Dictionary<string, Binary> assemblyMap = new Dictionary<string, Binary>();

        public event EventHandler<ReferringAssemblyStatusChangeEventArgs> ReferringAssemblyStatusChanged = delegate { };

        public IEnumerable<Binary> FindDependencies(string assemblyPath, bool recursive, out List<string> loadErrors)
        {
            loadErrors = new List<string>();
            assemblyMap.Clear();

            if (!File.Exists(assemblyPath))
            {
                loadErrors.Add("File not found: " + assemblyPath);
                return Enumerable.Empty<Binary>();
            }

            var assemblyDir = Path.GetDirectoryName(assemblyPath);
            var resolver = AssemblyInformationLoader.CreateAssemblyResolver(assemblyPath);

            try
            {
                using var mlc = new MetadataLoadContext(resolver);
                var assembly = mlc.LoadFromAssemblyPath(assemblyPath);
                FindDependencies(mlc, assembly, assemblyDir, recursive, loadErrors);
            }
            catch (Exception ex)
            {
                loadErrors.Add($"Failed to load {assemblyPath}: {ex.Message}");
            }

            var dependencies = assemblyMap.Values.OrderBy(p => p.FullName).ToList();

            foreach (var dependency in dependencies)
            {
                Trace.WriteLine($"{dependency.DisplayName} => {dependency.IsSystemBinary}");
            }

            return dependencies;
        }

        public IEnumerable<Binary> FindDependencies(AssemblyName assemblyName, string searchDirectory, bool recursive, out List<string> loadErrors)
        {
            loadErrors = new List<string>();
            assemblyMap.Clear();

            var assemblyPath = ResolveAssemblyPath(assemblyName, searchDirectory);
            if (assemblyPath == null)
            {
                loadErrors.Add("Failed to locate: " + assemblyName.FullName);
                return Enumerable.Empty<Binary>();
            }

            return FindDependencies(assemblyPath, recursive, out loadErrors);
        }

        public IEnumerable<string> FindReferringAssemblies(string testAssemblyFullName, string directory, bool recursive)
        {
            var referringAssemblies = new List<string>();
            var binaries = new List<string>();
            try
            {
                ReferringAssemblyStatusChanged(this, new ReferringAssemblyStatusChangeEventArgs { StatusText = "Finding all binaries" });
                FindAssemblies(new DirectoryInfo(directory), binaries, recursive);
            }
            catch (Exception)
            {
                UpdateProgress(Resource.FailedToListBinaries, -2);
                return null;
            }

            if (binaries.Count == 0)
            {
                return referringAssemblies;
            }

            var baseDirPathLength = directory.Length;
            if (!directory.EndsWith("\\"))
            {
                baseDirPathLength++;
            }

            var i = 0;
            foreach (var binary in binaries)
            {
                var message = String.Format(Resource.AnalyzingAssembly, Path.GetFileName(binary));
                var progress = (i++ * 100) / binaries.Count;
                if (progress == 100)
                {
                    progress = 99;
                }

                if (!UpdateProgress(message, progress))
                {
                    return referringAssemblies;
                }

                try
                {
                    var dw = new DependencyWalker();
                    var dependencies = dw.FindDependencies(binary, false, out _);

                    if (dependencies.Any(p => string.Compare(p.FullName, testAssemblyFullName, StringComparison.OrdinalIgnoreCase) == 0))
                    {
                        referringAssemblies.Add(binary.Remove(0, baseDirPathLength));
                    }
                }
                catch (ArgumentException) { }
                catch (FileLoadException) { }
                catch (FileNotFoundException) { }
                catch (BadImageFormatException) { }
            }

            return referringAssemblies.OrderBy(p => p);
        }

        private void FindDependencies(MetadataLoadContext mlc, Assembly assembly, string searchDirectory, bool recursive, List<string> loadErrors)
        {
            AssemblyName[] references;
            try
            {
                references = assembly.GetReferencedAssemblies();
            }
            catch (Exception ex)
            {
                loadErrors.Add($"Failed to get references from {assembly.GetName().Name}: {ex.Message}");
                return;
            }

            foreach (var referencedAssembly in references)
            {
                var name = referencedAssembly.FullName;

                if (assemblyMap.ContainsKey(name))
                {
                    continue;
                }

                assemblyMap[name] = new Binary(referencedAssembly);

                if (AssemblyInformationLoader.SystemAssemblies.Any(p => referencedAssembly.FullName.StartsWith(p)))
                {
                    assemblyMap[name].IsSystemBinary = true;
                    continue;
                }

                if (!recursive) continue;

                // Try to resolve and recurse
                var resolvedPath = ResolveAssemblyPath(referencedAssembly, searchDirectory);
                if (resolvedPath == null)
                {
                    // Try via MetadataLoadContext resolver (includes NuGet paths)
                    try
                    {
                        var refAsm = mlc.LoadFromAssemblyName(referencedAssembly);
                        if (!string.IsNullOrEmpty(refAsm.Location))
                            resolvedPath = refAsm.Location;
                    }
                    catch { }
                }
                if (resolvedPath != null)
                {
                    assemblyMap[name] = new Binary(referencedAssembly, resolvedPath);
                    try
                    {
                        var refAssembly = mlc.LoadFromAssemblyPath(resolvedPath);
                        FindDependencies(mlc, refAssembly, searchDirectory, true, loadErrors);
                    }
                    catch (Exception ex)
                    {
                        loadErrors.Add($"Failed to load {referencedAssembly.Name}: {ex.Message}");
                    }
                }
            }
        }

        private static string ResolveAssemblyPath(AssemblyName assemblyName, string searchDirectory)
        {
            if (string.IsNullOrEmpty(searchDirectory))
                return null;

            var dllPath = Path.Combine(searchDirectory, assemblyName.Name + ".dll");
            if (File.Exists(dllPath))
                return dllPath;

            var exePath = Path.Combine(searchDirectory, assemblyName.Name + ".exe");
            if (File.Exists(exePath))
                return exePath;

            return null;
        }

        private void FindAssemblies(DirectoryInfo directoryInfo, List<string> binaries, bool recursive)
        {
            var message = string.Format(Resource.AnalyzingFolder, directoryInfo.Name);
            if (!UpdateProgress(message, -1))
            {
                return;
            }

            binaries.AddRange(directoryInfo.GetFiles("*.dll").Select(fileInfo => fileInfo.FullName));
            binaries.AddRange(directoryInfo.GetFiles("*.exe").Select(fileInfo => fileInfo.FullName));

            if (!recursive) return;

            foreach (var directory in directoryInfo.GetDirectories())
            {
                FindAssemblies(directory, binaries, true);
            }
        }

        private bool UpdateProgress(string message, int progress)
        {
            if (null == ReferringAssemblyStatusChanged) return true;
            var eventArg = new ReferringAssemblyStatusChangeEventArgs { StatusText = message, Progress = progress };
            ReferringAssemblyStatusChanged(this, eventArg);
            return !eventArg.Cancel;
        }
    }
}
