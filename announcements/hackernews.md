# Hacker News "Show HN" Announcement

Category: Show HN
Best time: Weekday mornings (8-10 AM EST/PST)

Note: HN has NO markdown. Only: blank lines for paragraphs, *asterisks* for italics, 2-space indent for code, and bare URLs become links.

---

## Title (this goes in the title field)

Show HN: Windows shell extension for quick .NET assembly inspection

## URL (this goes in the url field)

https://github.com/tebjan/AssemblyInformation

## Text (leave EMPTY if submitting a URL - HN only allows one or the other)

HN link posts don't have a text body. The repo README is your post.
If you want to add context, post it as the first comment (see below).

---

## First Comment (post immediately after submitting)

Copy-paste this exactly (it uses only HN-compatible formatting):

```
Updated an old CodePlex tool (2008) that I use regularly. Adds "Assembly Information" to Windows Explorer's right-click menu for .NET DLLs/EXEs.

Shows compilation mode (Debug/Release), target framework, PE architecture, and dependency trees.

*The problem:* kept opening ILSpy just to check basic stuff - is this Debug or Release? What .NET version does it target? What dependencies does it have?

*Implementation:* uses MetadataLoadContext instead of Assembly.Load - reads metadata without executing code. Works across all .NET versions (Framework 2.0 through .NET 8+). C++ ATL/COM shell extension for Explorer integration. Safe for inspecting untrusted assemblies since it never loads them into the runtime.

Originally by rockrotem and Ashutosh Bhawasinka (2008-2012, CodePlex), migrated to GitHub by Jozef Izso (2014). Ms-PL license.

Handy when you're debugging dependency issues or stuck in DLL hell.
```

---

## HN Formatting Reference

- Blank lines separate paragraphs
- *asterisks* for italics (no bold)
- Two-space indent for code blocks
- Bare URLs become links automatically
- <angle brackets> if a URL doesn't link correctly
- No images, no headers, no bullet lists, no markdown

---

## Commenting Strategy

- Be present in the first 2 hours after posting
- Answer technical questions about MetadataLoadContext
- Give credit to original authors
- If someone asks about cross-platform: "Windows-only due to the COM shell extension, but the core inspection library is cross-platform"
- Let discussions develop naturally, don't reply to everything
