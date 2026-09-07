namespace Issue266.Common;

public static class StringHelper
{
    public static string ToHalfWidth(this string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        var chars = input.ToCharArray();
        for (var i = 0; i < chars.Length; i++)
        {
            if (chars[i] == '\u3000')
            {
                chars[i] = ' ';
            }
            else if (chars[i] is >= '\uFF01' and <= '\uFF5E')
            {
                chars[i] = (char)(chars[i] - 0xFEE0);
            }
        }

        return new string(chars);
    }

    public static bool IsBlank(this string? input) => string.IsNullOrWhiteSpace(input);
}
