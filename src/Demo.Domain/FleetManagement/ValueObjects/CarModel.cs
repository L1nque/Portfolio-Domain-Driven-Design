using Demo.Domain.FleetManagement.Enums;
using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.FleetManagement.ValueObjects;

/// <summary>
/// Encapsulates the Model of a car inside of a VO
/// </summary>
public class CarModel : ValueObject
{
    public string Brand { get; init; }
    public string Make { get; init; }
    public int Year { get; init; }
    public CarType Type { get; init; }

    public CarModel(string brand, string make, int year, CarType type)
    {
        Brand = brand;
        Make = make;
        Year = year;
        Type = type;
    }

    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return Brand;
        yield return Make;
        yield return Year;
        yield return Type;
    }
}
