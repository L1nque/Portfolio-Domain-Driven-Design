using Demo.SharedKernel.Core.Models;

namespace Demo.SharedKernel.Types;

/// <summary>
/// Represents a physical address.
/// This is a value object ensuring immutability and structural equality.
/// </summary>
public class Address : ValueObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Address"/> class.
    /// </summary>
    /// <param name="street">The street address line.</param>
    /// <param name="city">The city.</param>
    /// <param name="state">The state or province.</param>
    /// <param name="postalCode">The postal or ZIP code.</param>
    /// <param name="country">The ISO 3166-1 alpha-2 country code (e.g., "US", "GB").</param>
    public Address(string street, string city, string state, string postalCode, string country)
    {
        Street = street ?? throw new ArgumentNullException(nameof(street));
        City = city ?? throw new ArgumentNullException(nameof(city));
        State = state ?? throw new ArgumentNullException(nameof(state));
        PostalCode = postalCode ?? throw new ArgumentNullException(nameof(postalCode));
        Country = country ?? throw new ArgumentNullException(nameof(country));
    }

    /// <summary>
    /// Gets the street address line.
    /// </summary>
    public string Street { get; }

    /// <summary>
    /// Gets the city.
    /// </summary>
    public string City { get; }

    /// <summary>
    /// Gets the state or province.
    /// </summary>
    public string State { get; }

    /// <summary>
    /// Gets the postal or ZIP code.
    /// </summary>
    public string PostalCode { get; }

    /// <summary>
    /// Gets the ISO 3166-1 alpha-2 country code.
    /// </summary>
    public string Country { get; }

    /// <summary>
    /// Gets the atomic values for equality comparison.
    /// </summary>
    /// <returns>An enumerable containing all address components.</returns>
    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return Street;
        yield return City;
        yield return State;
        yield return PostalCode;
        yield return Country;
    }

    /// <summary>
    /// Returns a string representation of the address.
    /// </summary>
    /// <returns>A formatted string representing the address.</returns>
    public override string ToString()
    {
        return $"{Street}, {City}, {State} {PostalCode}, {Country}";
    }
}