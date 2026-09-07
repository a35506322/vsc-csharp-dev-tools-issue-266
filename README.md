# Repro: C# Dev Tools Test Explorer discovers only a subset of MSTest tests

Sample repository for [vsc-csharp-dev-tools#266](https://github.com/jakubkozera/vsc-csharp-dev-tools/issues/266).

Folder layout matches the original project (`src/` class library + `test/` MSTest):

```
src/Issue266.Common
test/Issue266.Common.UnitTests
```

## Stack

- .NET 10
- MSTest 4.3.2 + `Microsoft.NET.Test.Sdk` (VSTest, not MTP)
- Nested helper type named `TestModel` inside `[TestClass]` (private and public)

## Reproduce

1. Open `Issue266.slnx` in Cursor
2. Open Test Explorer and refresh
3. Compare with CLI:

```powershell
dotnet test "test/Issue266.Common.UnitTests" --list-tests --nologo
```

### Expected

CLI and Test Explorer show the same tests, grouped under the actual `[TestClass]` names (`RequiredTextAttributeTests`, `StringHelperTests`, ...).

### Actual (issue #266)

- CLI lists the full set
- Test Explorer shows only a subset
- Parent node is the nested helper type `TestModel`, not the `[TestClass]`
