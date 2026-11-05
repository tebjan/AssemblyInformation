# Assembly Information Modernization — Research Findings

Research conducted February 2026 to support migration from .NET Framework 4.7 to modern .NET.

---

## 1. Assembly Scanning: MetadataLoadContext + PEReader

### MetadataLoadContext
- **NuGet:** `System.Reflection.MetadataLoadContext` (latest: 10.0.2, use 8.0.x for net8.0)
- **Purpose:** Load and inspect assemblies as pure metadata — no code execution, no bitness restrictions
- **Key capability:** Inspects assemblies from ANY .NET version (.NET Framework 1.x–4.8, .NET Core, .NET 5–10, .NET Standard) from a single modern .NET app
- **Replaces:** `Assembly.ReflectionOnlyLoad` (throws `PlatformNotSupportedException` on .NET Core+), `Assembly.LoadFile` (loads into runtime, bitness-dependent)
- **Critical API notes:**
  - Must use `GetCustomAttributesData()` not `GetCustomAttributes()` (can't instantiate attribute types in metadata context)
  - Compare attribute types by string name: `attr.AttributeType.FullName == "System.Diagnostics.DebuggableAttribute"`
  - Requires a `PathAssemblyResolver` — must supply the target assembly's directory + framework reference assemblies
  - For .NET Framework assemblies: include `mscorlib.dll` from `C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\vX.X`
  - For .NET Core/.NET 5+ assemblies: include `System.Runtime.dll` from the runtime's shared framework

### PEReader (System.Reflection.PortableExecutable)
- **NuGet:** `System.Reflection.Metadata` (includes both PEReader and MetadataReader)
- **Purpose:** Read PE headers from any PE file regardless of .NET version
- **Key APIs:**
  - `PEHeaders.CoffHeader.Machine` — AMD64, I386, ARM, etc.
  - `PEHeaders.CorHeader.Flags` — ILOnly, Requires32Bit, Preferred32Bit, StrongNameSigned
  - `PEHeader.Magic` — PE32 vs PE32+
  - `PEReader.HasMetadata` — whether the file is a .NET assembly
- **Replaces:** `module.GetPEKind()` (deprecated) and manual PE header parsing via BinaryReader

### Comparison Table

| Approach | Cross-version? | Executes code? | Modern .NET? |
|----------|---------------|----------------|-------------|
| `Assembly.LoadFile` | No | Yes | Yes (risky) |
| `Assembly.ReflectionOnlyLoad` | Partially | No | No (throws) |
| `MetadataLoadContext` | **Yes — any version** | No | Yes |
| `PEReader` | **Yes — any PE file** | No | Yes |

---

## 2. Library Replacements

### MVVM Light → CommunityToolkit.Mvvm
- MVVM Light is **discontinued** by Laurent Bugnion
- **Replacement:** `CommunityToolkit.Mvvm` (v8.4.0) — official Microsoft successor
- Migration: `ViewModelBase` → `ObservableObject`, `RelayCommand` → `RelayCommand` (same name, different namespace)
- Source generators: `[ObservableProperty]`, `[RelayCommand]` for boilerplate reduction
- [Migration guide](https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/migratingfrommvvmlight)

### StructureMap → Microsoft.Extensions.DependencyInjection
- StructureMap is **deprecated/sunsetted**
- Its successor Lamar is also entering maintenance mode
- **Replacement:** `Microsoft.Extensions.DependencyInjection` — the .NET ecosystem standard

### PropertyChanged.Fody
- Still works on modern .NET (v4.1.0, targets netstandard2.0)
- **Alternative:** CommunityToolkit.Mvvm `[ObservableProperty]` source generator (compile-time, no IL weaving)

---

## 3. Shell Extension Approaches

### Classic Context Menu (IContextMenu)
- Traditional COM shell extension implementing `IShellExtInit` + `IContextMenu`
- Works on all Windows versions
- On Windows 11: appears in "Show more options" fallback menu
- Must match Explorer.exe bitness (64-bit on 64-bit Windows)
- Registration: HKLM\SOFTWARE\Classes\CLSID\{GUID} (requires admin/elevation)

### Windows 11 Top-Level Context Menu (IExplorerCommand)
- Requires implementing `IExplorerCommand` interface
- Requires **package identity** via full MSIX or Sparse MSIX package
- Appears in the new Windows 11 context menu directly
- Reference implementation: [VS Code's IExplorerCommand](https://github.com/microsoft/vscode-explorer-command)

### Sparse MSIX Packages
- Grants package identity to unpackaged Win32 apps
- `AppxManifest.xml` references files at external location (install directory)
- Available since Windows 10 version 2004
- Must be signed (self-signed for dev, trusted cert for production)
- Set `TrustLevel="mediumIL"` and `RuntimeBehavior="win32App"`
- Register via `PackageManager.AddPackageByUriAsync()` with `ExternalLocationUri`

### .NET Shell Extensions
- **SharpShell** (v2.7.2) — .NET Framework focused, experimental .NET 8 support
- **.NET 8+ COM source generators** (`GeneratedComInterfaceAttribute`) — AOT-compatible, but no shell extension framework exists yet
- **CBFS Shell** — commercial component supporting .NET 6–10
- **Recommendation:** Keep C++ for shell extensions (most reliable for in-process Explorer loading)

### Rights Management
- COM registration via MSI `Class` table (WiX) — no manual regsvr32, elevation handled by Windows Installer service
- Shell extension DLLs should be code-signed for SmartScreen reputation and Windows Defender trust
- EV code signing no longer provides instant SmartScreen reputation (changed August 2024)
- **Microsoft Trusted Signing:** Cloud-based signing, $9.99/month basic plan

---

## 4. Installer: WiX Toolset v6

### Version Info
- **WiX v6.0.2** — current stable (August 28, 2025)
- **WiX v7.0.0-rc.1** — prerelease (February 6, 2026)
- Requires .NET SDK 6+ for build tooling

### Key NuGet Packages
| Package | Purpose |
|---------|---------|
| `WixToolset.Sdk` (6.0.2) | MSBuild SDK for .wixproj |
| `WixToolset.UI.wixext` (6.0.2) | Standard UI dialogs |
| `WixToolset.Util.wixext` (6.0.2) | Utility extension |

### Advantages over .vdproj
- Native COM registration via MSI Class/TypeLib tables (no regsvr32 custom actions)
- Proper UAC elevation via Windows Installer service
- SDK-style project integration
- CI/CD friendly (builds from command line)
- Active development vs deprecated .vdproj

### MSIX Limitations
- Cannot register in-process shell extensions (registry is virtualized)
- Per-user by design; per-machine requires provisioning
- Not suitable as standalone installer for COM shell extensions
- Sparse MSIX can complement a traditional MSI installer

---

## 5. UI Framework Comparison (for future reference)

### WinForms on Modern .NET
- Fully supported on .NET 8/9/10
- Dark mode finalized in .NET 10 (`Application.SetColorMode()`)
- Async forms finalized in .NET 10
- Classic Windows look, GDI+ rendering
- Raster-based — may have DPI scaling issues on mixed-DPI setups

### WPF on Modern .NET
- Fully supported, more active investment than WinForms
- Fluent theme since .NET 9 (Windows 11 look, dark/light mode)
- Fluent theme still "in progress" in .NET 10 — more controls styled but not all
- No native Mica/Acrylic (DWM backdrop hack only)
- Vector-based rendering, excellent DPI scaling
- Full MVVM/data binding support

### Avalonia UI
- Latest stable: 11.3.11 (January 2026)
- Cross-platform: Windows, macOS, Linux, WebAssembly
- Built-in FluentTheme with dark/light mode
- Compiled bindings (catch errors at compile time)
- Uses CommunityToolkit.Mvvm
- TreeView doesn't virtualize (use TreeDataGrid for large datasets, but requires paid license since 11.2)
- Self-rendering via SkiaSharp (GPU-accelerated)
- Avalonia 12 planned for Q4 2026 with GPU-first rendering

### Recommendation
- **Windows-only tool (current):** WinForms for minimal migration, WPF for modern look
- **Cross-platform future:** Avalonia (MetadataLoadContext/PEReader are fully cross-platform)
- **Avoid:** WinUI 3 (stability concerns), MAUI (overkill for Windows-only)

---

## 6. .NET Version Landscape (February 2026)

| Version | Status | Support Until |
|---------|--------|--------------|
| .NET 8 | LTS (current) | November 2026 |
| .NET 9 | STS | May 2026 |
| .NET 10 | **LTS (latest)** | November 2028 |

- .NET 10 GA: November 11, 2025
- `net10.0-windows` is the latest stable TFM
- All packages (MetadataLoadContext, CommunityToolkit.Mvvm, WiX) support .NET 10
