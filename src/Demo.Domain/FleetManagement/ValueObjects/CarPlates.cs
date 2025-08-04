using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.FleetManagement.ValueObjects;

/// <summary>
/// Encapsulates the plates of a car into a VO
/// </summary>
public class CarPlates : ValueObject
{
    public string Number { get; init; }
    public char Code { get; init; }
    public string City { get; init; }

    public CarPlates(string number, char code, string city)
    {
        Number = number;
        Code = code;
        City = city;
    }

    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return Number;
        yield return Code;
        yield return City;
    }
}
