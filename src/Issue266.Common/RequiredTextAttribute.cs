using System.ComponentModel.DataAnnotations;

namespace Issue266.Common;

public sealed class RequiredTextAttribute : ValidationAttribute
{
    public override bool IsValid(object? value) =>
        value is string text && !string.IsNullOrWhiteSpace(text);
}
