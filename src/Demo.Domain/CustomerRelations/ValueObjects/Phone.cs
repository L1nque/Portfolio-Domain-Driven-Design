using Demo.Domain.CustomerRelations.Enums;
using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.CustomerRelations.ValueObjects;

public class Phone : ValueObject
{
    public string Number { get; }
    public PhoneType Type { get; }
    public bool IsPrimary { get; }
    public string CountryCode { get; }

    public Phone(string number, PhoneType type, bool isPrimary, string countryCode)
    {
        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Phone number cannot be null or empty", nameof(number));

        if (string.IsNullOrWhiteSpace(countryCode))
            throw new ArgumentException("Country code cannot be null or empty", nameof(countryCode));

        Number = number;
        Type = type;
        IsPrimary = isPrimary;
        CountryCode = countryCode;
    }

    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return CountryCode;
        yield return Number;
        yield return Type;
        yield return IsPrimary;
    }
}