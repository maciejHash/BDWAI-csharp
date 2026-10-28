public static class StringExtensions // lab03 zadanie 4.1
{
    public static string ReverseText(this string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        return new string(text.Reverse().ToArray());
    }

    public static int CountWords(this string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        return string.IsNullOrWhiteSpace(text)
            ? 0
            : text.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries).Length;
    }
}
