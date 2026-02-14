# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

.NET Assembly Information — a Windows Explorer shell extension that adds an "Assembly Information" context menu entry to .NET assemblies. Displays compilation mode (Debug/Release), full assembly name, framework version, PE kind, target processor, and assembly references (including recursive/transitive dependencies). Licensed under Ms-PL.

## Build Commands

The .NET projects target **net8.0-windows** and use SDK-style project files.

```bash
# Build the .NET solution
dotnet build AssemblyInformation.sln -c Release

# Publish as self-contained single-file exe
dotnet publish AssemblyInformation/AssemblyInformation.csproj -c Release -r win-x64 --self-contained -p:PublishSingleFile=true -o publish
```

The C++ shell extension (`AssemblyInformationShellExt/`) requires Visual Studio with C++ ATL/COM workload and builds separately via MSBuild:

```bash
msbuild AssemblyInformationShellExt/AssemblyInformation.vcxproj /p:Configuration=Release /p:Platform=x64
```

There are **no automated tests** in this project.

## Architecture

Two .NET projects in `AssemblyInformation.sln`:

- **AssemblyInformation** (WinForms, net8.0-windows) — Entry point. Takes a file path argument, validates it's a .NET assembly via `PEReader.HasMetadata`, then opens `FormMain` with the file path. Single AnyCPU executable.
- **AssemblyInformation.Model** (net8.0) — Core inspection logic using **MetadataLoadContext** and **PEReader** (no runtime assembly loading). `AssemblyInformationLoader` extracts metadata (DebuggableAttribute, TargetFrameworkAttribute, PE headers, references). `DependencyWalker` recursively walks assembly references. Can inspect assemblies from any .NET version (.NET Framework, .NET Core, .NET 5+, .NET Standard).

Additional project (separate from .NET solution):

- **AssemblyInformationShellExt** — C++ ATL/COM shell extension (IContextMenu) for Windows Explorer context menu. x64 only, v143 toolset.

## Key Technical Patterns

- Assembly inspection uses `MetadataLoadContext` with `PathAssemblyResolver` — never loads assemblies into the runtime
- PE headers read via `System.Reflection.PortableExecutable.PEReader` for machine type and CorFlags
- Custom attributes read via `GetCustomAttributesData()` with string-based type name comparison (MetadataLoadContext cannot instantiate attributes)
- `AssemblyInformationLoader.CreateAssemblyResolver()` builds the resolver from: target assembly directory + .NET runtime directory + .NET Framework reference assemblies

## CI/CD

GitHub Actions workflows in `.github/workflows/`:

- `build.yml` — builds on push to master and PRs
- `release.yml` — creates GitHub releases with self-contained single-file exe on version tags (`v*`)
