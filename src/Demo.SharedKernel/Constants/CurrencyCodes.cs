namespace Demo.SharedKernel.Constants;

/// <summary>
/// Provides a list of common ISO 4217 currency codes.
/// </summary>
public static class CurrencyCodes
{
    public const string AED = "AED";
    public const string USD = "USD";
    public const string EUR = "EUR";
    public const string GBP = "GBP";
    public const string JPY = "JPY";
    public const string CAD = "CAD";
    public const string AUD = "AUD";
    public const string CHF = "CHF";
    public const string CNY = "CNY";
    public const string SEK = "SEK";
    public const string NZD = "NZD";

    public const string Default = AED;


    /// <summary>
    /// A collection of all defined currency codes.
    /// </summary>
    private static readonly HashSet<string> ValidCodes = new(StringComparer.OrdinalIgnoreCase)
    {
        AED, USD, EUR, GBP, JPY, CAD, AUD, CHF, CNY, SEK, NZD
    };

    /// <summary>
    /// Checks if a given string is a valid currency code.
    /// </summary>
    /// <param name="code">The currency code to validate.</param>
    /// <returns>true if the code is valid; otherwise, false.</returns>
    public static bool IsValid(string code)
    {
        return !string.IsNullOrWhiteSpace(code) && ValidCodes.Contains(code);
    }
}