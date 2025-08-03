using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.RentalContracting.ValueObjects;


/// <summary>
/// A Value Object representing the fuel level of a car.
/// Also contains domain logic, which in this case is that fuel must be 
/// represented as a value between 0-1
/// </summary>
public class FuelLevel : ValueObject
{
    public float Value { get; init; }

    public FuelLevel(int value)
    {
        if (Value > 1)
        {
            // Ideally convert this into a DomainException
            throw new ArgumentException("Fuel must be represented as a value between 0 to 1");
        }

        Value = value;
    }

    public static bool operator >(FuelLevel current, FuelLevel other)
    {
        if (current == null || other == null)
        {
            return false;
        }

        return current.Value > other.Value;
    }

    public static bool operator <(FuelLevel current, FuelLevel other)
    {
        if (current == null || other == null)
        {
            return false;
        }

        return current.Value < other.Value;
    }

    public static bool operator >=(FuelLevel current, FuelLevel other)
    {
        if (current == null || other == null)
        {
            return false;
        }

        return current.Value >= other.Value;
    }

    public static bool operator <=(FuelLevel current, FuelLevel other)
    {
        if (current == null || other == null)
        {
            return false;
        }

        return current.Value <= other.Value;
    }

    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return Value;
    }
}