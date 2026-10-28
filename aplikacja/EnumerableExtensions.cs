public static class EnumerableExtensions // lab03 zadanie 4.2
{
    public static bool IsEmpty<T>(this IEnumerable<T> source)
    {
        ArgumentNullException.ThrowIfNull(source);

        return !source.Any();
    }

    public static double Mean(this IEnumerable<int> source) // zadanie 4.3
    {
        ArgumentNullException.ThrowIfNull(source);

        return source.Average();
    }

    public static float Mean(this IEnumerable<float> source) // zadanie 4.4
    {
        ArgumentNullException.ThrowIfNull(source);

        return source.Average();
    }

    public static double MeanLength(this IEnumerable<string> source) // zadanie 4.5
    {
        ArgumentNullException.ThrowIfNull(source);

        return source.Average(text => text.Length);
    }
}
