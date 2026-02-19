# .NET Assembly Information

Windows Explorer extension that adds the entry "Assembly Information" to the context menu of .NET assemblies:

![context menu](https://raw.githubusercontent.com/tebjan/AssemblyInformation/master/images/context_menu.png)

Displays .NET Assembly information like:

1. Compilation mode (Debug/Release)
2. .NET Assembly full name
3. Framework version and PE kind
4. .NET Assembly references (even recursively)

![ui](https://raw.githubusercontent.com/tebjan/AssemblyInformation/master/images/ui.png)

Works with assemblies from all .NET versions: .NET Framework, .NET Core, .NET 5+, and .NET Standard. Also shows Win32 version info for native PE files.

Supports dark mode (configurable via View > Theme menu).

## Installation

Download the latest release from [GitHub Releases](https://github.com/tebjan/AssemblyInformation/releases) and run `install.cmd` as Administrator. This registers the Explorer context menu for .dll and .exe files.

Requires [.NET 10 Desktop Runtime](https://dotnet.microsoft.com/download/dotnet/10.0) or newer.

To uninstall, run `uninstall.cmd` as Administrator.

## Building

Requires .NET 10 SDK.

```bash
dotnet build AssemblyInformation.sln -c Release
```

## Usage

```bash
AssemblyInformation.exe <path-to-assembly.dll>
```

Or right-click a .dll/.exe in Windows Explorer and select "Assembly Information" (requires installation, see above).

You can also drag and drop files onto the application window, or use Options > Open Assembly (Ctrl+O).

## License

.NET Assembly Information source code is licensed under [Microsoft Public License (Ms-PL)](LICENSE.txt)
