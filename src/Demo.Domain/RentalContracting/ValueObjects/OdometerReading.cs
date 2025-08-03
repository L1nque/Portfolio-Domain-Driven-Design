using Demo.Domain.RentalContracting.Enums;
using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.RentalContracting.ValueObjects;

/// <summary>
/// ValueObject that is used to Snapshot the <see cref="Entities.RentalCar.InitialOdometer"/>
/// and the <see cref="Entities.RentalCar.FinalOdometer"/>
/// </summary>
public class OdometerReading : ValueObject
{
    public static readonly float ConversionRatio = 1.60934f;

    /// <summary>
    /// Some cars, e.g. American Specs use mileage,
    /// while others use KM
    /// </summary>
    public OdometerUnits Units { get; init; }
    public float Value { get; init; }

    public OdometerReading(OdometerUnits units, float value)
    {
        Units = units;
        Value = value;
    }

    public float ToMiles()
    {
        return Units == OdometerUnits.Miles ? Value
            : Value / ConversionRatio;
    }

    public float ToKilometers()
    {
        return Units == OdometerUnits.Km ? Value
            : Value * ConversionRatio;
    }

    public static bool operator >(OdometerReading current, OdometerReading other)
    {
        if (current == null || other == null)
        {
            return false;
        }

        return current.Value > other.Value;
    }

    public static bool operator >=(OdometerReading current, OdometerReading other)
    {
        if (current == null || other == null)
        {
            return false;
        }

        return current.Value >= other.Value;
    }

    public static bool operator <(OdometerReading current, OdometerReading other)
    {
        if (current == null || other == null)
        {
            return false;
        }

        return current.Value < other.Value;
    }

    public static bool operator <=(OdometerReading current, OdometerReading other)
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
        yield return Units;
    }
}
