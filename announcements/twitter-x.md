# Twitter/X Announcement Thread

**Account**: @tebjan
**Best time**: Weekday mornings (9-11 AM EST) or Tuesday/Wednesday
**Hashtags**: #dotnet #csharp #opensource

---

## Tweet 1 (Main)

Updated an old .NET tool I use regularly

Right-click any DLL/EXE → "Assembly Information"
• Debug vs Release
• Target framework
• Dependencies
• PE architecture

Beats opening ILSpy just to check basic stuff

https://github.com/tebjan/AssemblyInformation

[Attach: context_menu.png or ui.png]

#dotnet #csharp

---

## Tweet 2 (Technical details - reply to Tweet 1)

Uses MetadataLoadContext instead of Assembly.Load
→ Reads metadata without executing code
→ Works across all .NET versions (Framework 2.0 → .NET 8+)
→ No file locks

Helpful when:
• Checking if builds are actually Release
• Debugging dependency issues
• Stuck in DLL hell

---

## Tweet 3 (History - reply to Tweet 2)

Originally by rockrotem (2008, CodePlex) and Ashutosh Bhawasinka (2011-12). Migrated to GitHub by Jozef Izso (2014).

Ms-PL license

---

## Tweet 4 (Call to action - reply to Tweet 3)

Link: https://github.com/tebjan/AssemblyInformation/releases

What do you use for quick assembly checks?

---

## Alternative: Single Tweet Version

.NET assembly inspector for Windows

Right-click DLL/EXE → Debug/Release mode, framework version, dependencies

Uses MetadataLoadContext. Works with all .NET versions.

Originally by rockrotem (2008), updated to .NET 8.

https://github.com/tebjan/AssemblyInformation

#dotnet #csharp

[Attach screenshot]

---

## Engagement Strategy
- Tag @dotnet official account (use sparingly, maybe for launch only)
- Reply to comments within first hour
- If it gets traction, quote-tweet with use cases or technical deep-dives
- Share on LinkedIn simultaneously
- Ask followers to RT/share
