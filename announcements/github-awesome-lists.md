# GitHub Awesome Lists Submissions

## awesome-dotnet

**Repository**: https://github.com/quozd/awesome-dotnet
**Submission Method**: Pull Request
**Category**: Tools > Decompilation

---

### PR Title
Add .NET Assembly Information - Windows shell extension for assembly inspection

---

### PR Description

Adding **Assembly Information** to the Decompilation/Tools section.

**What it is:**
Windows Explorer shell extension for quick .NET assembly metadata inspection.

**Key features:**
- Right-click context menu integration for .NET assemblies
- Shows Debug/Release compilation mode, target framework, PE kind
- Displays full dependency tree (recursive/transitive)
- Uses MetadataLoadContext for safe inspection without runtime loading
- Works with all .NET versions (.NET Framework, Core, 5-8+, Standard)

**Repository:** https://github.com/tebjan/AssemblyInformation
**License:** Ms-PL
**Stars:** 20+
**Actively maintained:** Yes (latest release Feb 2026)

---

### Entry to Add

Under **Tools > Decompilation** section:

```markdown
* [Assembly Information](https://github.com/tebjan/AssemblyInformation) - Windows Explorer shell extension to view .NET assembly metadata, compilation mode (Debug/Release), target framework, and dependencies without loading assemblies into runtime.
```

---

## awesome-csharp

**Repository**: https://github.com/uhub/awesome-csharp
**Submission Method**: Pull Request
**Category**: Tools

---

### Entry to Add

```markdown
* [Assembly Information](https://github.com/tebjan/AssemblyInformation) - Windows shell extension for inspecting .NET assembly metadata, framework targets, and dependency trees.
```

---

## Other Awesome Lists to Consider

### awesome-dot-net-core
**Repository**: https://github.com/thangchung/awesome-dotnet-core
**Category**: Tools

```markdown
* [Assembly Information](https://github.com/tebjan/AssemblyInformation) - Quick assembly metadata inspection via Windows Explorer context menu.
```

---

### Guidelines for Awesome List PRs

1. **Keep descriptions concise** (< 120 characters)
2. **Highlight unique value** - what makes it different
3. **Follow existing format** - match the style of other entries
4. **Include proper license** - Ms-PL in our case
5. **Ensure alphabetical order** within the section
6. **One tool per PR** - easier to review
7. **Be patient** - maintainers are volunteers

---

## Submission Checklist

- [ ] Fork the awesome list repository
- [ ] Create a new branch (`add-assembly-information`)
- [ ] Add entry in correct alphabetical position
- [ ] Ensure description is concise and clear
- [ ] Follow existing formatting exactly
- [ ] Create PR with clear title
- [ ] Wait for maintainer review
- [ ] Respond to feedback promptly
