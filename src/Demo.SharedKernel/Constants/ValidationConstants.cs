namespace Demo.SharedKernel.Constants;

/// <summary>
/// Defines shared validation rules and constraints used across the application.
/// </summary>
public static class ValidationConstants
{
    /// <summary>
    /// Constants related to name validation.
    /// </summary>
    public static class Names
    {
        /// <summary>
        /// The maximum allowed length for a person's first or last name.
        /// </summary>
        public const int MaxLength = 100;
    }

    /// <summary>
    /// Constants related to email validation.
    /// </summary>
    public static class Email
    {
        /// <summary>
        /// The maximum allowed length for an email address.
        /// </summary>
        public const int MaxLength = 254; // RFC 5321 limit
    }

    /// <summary>
    /// Constants related to address validation.
    /// </summary>
    public static class Address
    {
        /// <summary>
        /// The maximum allowed length for an address line (street).
        /// </summary>
        public const int MaxStreetLength = 200;

        /// <summary>
        /// The maximum allowed length for a city name.
        /// </summary>
        public const int MaxCityLength = 100;

        /// <summary>
        /// The maximum allowed length for a state or province name.
        /// </summary>
        public const int MaxStateLength = 100;

        /// <summary>
        /// The maximum allowed length for a postal or ZIP code.
        /// </summary>
        public const int MaxPostalCodeLength = 20;

        /// <summary>
        /// The maximum allowed length for a country code.
        /// </summary>
        public const int MaxCountryLength = 2; // ISO Alpha-2
    }

    /// <summary>
    /// Constants related to currency and money validation.
    /// </summary>
    public static class Money
    {
        /// <summary>
        /// The standard scale (number of decimal places) for monetary amounts.
        /// </summary>
        public const int Scale = 2;

        /// <summary>
        /// The maximum precision (total number of digits) for monetary amounts.
        /// </summary>
        public const int Precision = 18; // default for decimal by most ORMs (e.g. EFCore)
    }
}