namespace Demo.SharedKernel.Extensions;

/// <summary>
/// Provides extension methods for string manipulation.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Converts the first character of the string to uppercase using the invariant culture.
    /// </summary>
    /// <param name="input">The string to convert.</param>
    /// <returns>The string with the first character capitalized, or the original string if it's null or empty.</returns>
    public static string ToTitleCase(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        return char.ToUpperInvariant(input[0]) + input.Substring(1);
    }

    /// <summary>
    /// Checks if the string is null, empty, or consists only of white-space characters.
    /// This is a convenience wrapper around string.IsNullOrWhiteSpace.
    /// </summary>
    /// <param name="input">The string to check.</param>
    /// <returns>true if the string is null, empty, or whitespace; otherwise, false.</returns>
    public static bool IsNullOrWhiteSpace(this string? input)
    {
        return string.IsNullOrWhiteSpace(input);
    }
}