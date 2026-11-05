# YouTube Demo Video

**Video Type**: Quick demo/tutorial (2-3 minutes)
**Channel**: @tebjan
**Format**: Screen recording with voiceover

---

## Video Title

.NET Assembly Inspector - Quick Metadata Checks from Windows Explorer

---

## Video Description

Quick demo of Assembly Information - a Windows Explorer shell extension for .NET developers.

Right-click any .NET DLL/EXE to instantly see:
✅ Debug vs Release compilation mode
✅ Target framework version
✅ Assembly identity & versioning
✅ PE characteristics (x86/x64/AnyCPU)
✅ Full dependency tree

Perfect for build verification, dependency auditing, and framework compatibility checks.

Download: https://github.com/tebjan/AssemblyInformation/releases
GitHub: https://github.com/tebjan/AssemblyInformation
License: Microsoft Public License (Ms-PL) - open source

⏱️ Timestamps:
0:00 - Introduction
0:20 - Installation
0:45 - Context menu integration
1:10 - Debug/Release detection
1:30 - Framework targeting
1:50 - Dependency tree navigation
2:20 - Technical details (MetadataLoadContext)
2:50 - Conclusion

Built with:
- .NET 8
- System.Reflection.MetadataLoadContext
- C++ ATL/COM shell extension

Credits:
Originally created by rockrotem (2008) and Ashutosh Bhawasinka (2011-12) on CodePlex, migrated by Jozef Izso (2014), modernized to .NET 8 by Tebjan Halm (2025-26).

#dotnet #csharp #programming #developer #tools #visualstudio #windows

---

Subscribe for more .NET, graphics programming, and real-time AI content.

Links:
- GitHub: https://github.com/tebjan
- Twitter/X: https://x.com/tebjan
- LinkedIn: https://linkedin.com/in/tebjan
- Website: https://tebjan.de

---

## Video Script

**[0:00] Introduction**

"Hey everyone, Tebjan here. Today I'm showing you a quick tool I modernized for .NET developers on Windows.

If you've ever needed to check if a DLL is Debug or Release, or see what framework it targets, you probably opened ILSpy or dnSpy. But for simple metadata checks, that's overkill.

Let me show you a faster way."

**[0:20] Installation**

"Assembly Information is a Windows Explorer shell extension. Download the latest release from GitHub, extract the exe, and you're ready to go.

For the shell extension, you'll need to register it - instructions are in the README."

**[0:45] Context Menu**

"Now, right-click any .NET DLL or EXE in Windows Explorer... and you'll see 'Assembly Information' in the context menu.

Click it, and boom - instant metadata."

**[1:10] Debug/Release**

"The first thing I check is usually the compilation mode. This DLL shows 'Release' - perfect.

It checks the DebuggableAttribute to determine this. Super useful for build verification."

**[1:30] Framework Targeting**

"Next, the target framework. This one targets .NET 8.0. But it works with ANY .NET version - Framework 2.0 all the way to .NET 8+, Core, Standard - you name it."

**[1:50] Dependencies**

"The dependency tree shows all references - and you can expand to see transitive dependencies.

Double-click any assembly to inspect it. Great for dependency auditing or finding version conflicts."

**[2:20] Technical Details**

"Under the hood, it uses MetadataLoadContext instead of Assembly.Load. This means it never executes code - totally safe for inspecting untrusted assemblies. And it doesn't lock files either."

**[2:50] Conclusion**

"That's it! A simple tool that integrates right into your workflow.

Originally created by rockrotem in 2008, enhanced by Ashutosh Bhawasinka, and I've updated it to .NET 8 with modern APIs.

It's open source under the Ms-PL license - link in the description.

If you find it useful, star the repo and let me know in the comments what other .NET tools you'd like to see!

Thanks for watching!"

---

## Video Production Notes

**Screen Recording Setup:**
- Use OBS or Camtasia
- 1920x1080 resolution
- 60fps for smooth cursor movement
- Highlight cursor for visibility

**What to Show:**
1. GitHub releases page
2. Extract and run exe
3. Right-click on a sample DLL (show context menu clearly)
4. Click "Assembly Information"
5. Walk through each section of the UI
6. Show Debug vs Release comparison (two different DLLs)
7. Show different framework targets (.NET Framework 4.8, .NET 8, .NET Standard)
8. Expand dependency tree
9. Double-click a reference to show navigation
10. Quick code snippet of MetadataLoadContext usage

**Sample Assemblies to Demo:**
- A Debug-compiled DLL
- A Release-compiled DLL
- .NET Framework 4.8 assembly
- .NET 8 assembly
- .NET Standard 2.0 library
- Assembly with many dependencies (e.g., ASP.NET Core app)

**Post-Production:**
- Add subtle background music (low volume)
- Add text overlays for key points
- Add timestamps in description
- Add end screen with subscribe button and GitHub link
- Add chapters/sections

**Thumbnail Ideas:**
- Screenshot of the context menu with "Assembly Information" highlighted
- Split screen: ILSpy vs Assembly Information (showing speed difference)
- Large text: "Quick .NET Metadata Checks"
