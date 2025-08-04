using Demo.Domain.FleetManagement.Enums;
using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.FleetManagement.ValueObjects;

/// <summary>
/// VO that encapsulates the fuel level of a car, tank size, and type.
/// </summary>
public class FuelTank : ValueObject
{
    public static readonly int MinimumFuelThresholdForTrips = 20;

    public float Level { get; init; }
    public float Capacity { get; init; }
    public FuelType Type { get; init; }
    public bool IsTripSufficient => Level > MinimumFuelThresholdForTrips;

    public FuelTank(FuelType type, float capacity, float level)
    {
        Type = type;
        Capacity = capacity;
        Level = level;
    }

    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return Level;
        yield return Capacity;
    }
}
