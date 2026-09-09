# Repro: C# Dev Tools Test Explorer / MSTest DataRow

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
- `[TestMethod]` + many `[DataRow]` whose first argument is a JSON-like string with escaped quotes

## Reproduce

1. Open `Issue266.slnx` in Cursor
2. Open Test Explorer and refresh
3. Compare with CLI:

```powershell
dotnet test "test/Issue266.Common.UnitTests" --list-tests --nologo
```

### Expected

CLI and Test Explorer show the same tests.

### Actual — original #266 (nested `TestModel`) — **fixed**

Previously Test Explorer showed only a subset, parent node was nested helper type `TestModel`.

After the fix, the original 5 classes list correctly (56 tests):

| Class                         | Tests |
| ----------------------------- | ----- |
| `MoreStringHelperTests`       | 12    |
| `ObjectMapperTests`           | 12    |
| `RequiredTextAttributeTests`  | 7     |
| `StringHelperTests`           | 15    |
| `ValidTextListAttributeTests` | 10    |

### Actual — follow-up: `DataRow` display / incomplete run

See `AddressHelperTests`. CLI lists every DataRow. Test Explorer does not.

| Method                                            | CLI DataRows | Test Explorer                                                                  |
| ------------------------------------------------- | ------------ | ------------------------------------------------------------------------------ |
| `FindZipCode_簡單參數_Should正常列出`             | 3            | Control: plain string args, should list normally                               |
| `FindZipCode_無法命中_Should回傳後兩碼歸零`       | 5            | Labels truncate (`... ("`), some children stay unrun / look like leftover JSON |
| `FindZipCode_符合範圍模板_Should回傳對應郵遞區號` | 120          | Some DataRows missing; cannot run the full set from Test Explorer              |

CLI total after this follow-up: **184** (56 original + 128 DataRows). All 184 pass via `dotnet test`.

Trigger: first `DataRow` argument is a JSON array string, e.g. `"[\"of\",\"1\",\"to\",\"of\",\"3\"]"`. Default MSTest display name embeds that quoted string, and Test Explorer parsing/display breaks.

These tests only assert the parameter shape. They exist to reproduce discovery/display, not zip-code logic.
