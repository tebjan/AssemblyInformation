# .NET Assembly Information

[![Build](https://github.com/tebjan/AssemblyInformation/actions/workflows/build.yml/badge.svg)](https://github.com/tebjan/AssemblyInformation/actions/workflows/build.yml)

Windows Explorer extension that adds the entry "Assembly Information" to the context menu of .NET assemblies:

![context menu](https://raw.githubusercontent.com/tebjan/AssemblyInformation/master/images/context_menu.png)

Displays .NET Assembly information like:

1. Compilation mode (Debug/Release)
2. .NET Assembly full name
3. Framework version and PE kind
4. .NET Assembly references (even recursively)

![ui](https://raw.githubusercontent.com/tebjan/AssemblyInformation/master/images/ui.png)

Works with assemblies from all .NET versions: .NET Framework, .NET Core, .NET 5/6/7/8+, and .NET Standard.

## Building

Requires .NET 8 SDK.

```bash
dotnet build AssemblyInformation.sln -c Release
```

## Usage

```bash
AssemblyInformation.exe <path-to-assembly.dll>
```

Or right-click a .dll/.exe in Windows Explorer and select "Assembly Information" (requires shell extension registration).

## License

.NET Assembly Information source code is licensed under [Microsoft Public License (Ms-PL)](LICENSE.txt)
