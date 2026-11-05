# .NET Foundation Discord Announcement

**Server**: https://discord.gg/dotnet-foundation
**Channel**: #show-and-tell or #dotnet-general
**Format**: Casual, community-friendly

---

## Message

Hey folks

Updated an old CodePlex tool I use regularly - **Assembly Information**

Windows Explorer shell extension for .NET assemblies. Right-click any DLL/EXE → shows metadata:
- Debug vs Release
- Target framework
- Dependencies (recursive tree)
- PE characteristics

Saves me from opening ILSpy just to check if something is Debug or Release.

Uses `MetadataLoadContext` instead of `Assembly.Load` - safe to inspect anything, works across all .NET versions.

GitHub: https://github.com/tebjan/AssemblyInformation
Originally by rockrotem (2008) & Ashutosh Bhawasinka (2011-12)
Ms-PL license

Questions welcome.

---

## Follow-up if someone asks technical questions

**About MetadataLoadContext:**
```csharp
// Instead of Assembly.Load (dangerous, locks files):
var resolver = new PathAssemblyResolver(searchPaths);
using var mlc = new MetadataLoadContext(resolver);
var asm = mlc.LoadFromAssemblyPath(path);
// Safe inspection, no code execution!
```

**Cross-platform?**
Sadly Windows-only for now due to the shell extension being COM-based. The core inspection library (AssemblyInformation.Model) is cross-platform though - could be used for CLI tools on Linux/macOS.

**Why not ILSpy/dnSpy?**
Those are amazing for full decompilation. This is for quick metadata checks - 2 clicks vs launching a whole app. Different use cases.

---

## Engagement Tips
- Be humble and friendly
- Give credit to original authors
- Answer technical questions in threads
- Share use cases from your own work
- If someone suggests a feature, consider creating a GitHub issue
- Thank people for trying it out
