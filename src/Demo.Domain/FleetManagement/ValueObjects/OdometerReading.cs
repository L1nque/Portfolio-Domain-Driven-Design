using Demo.Domain.FleetManagement.Enums;
using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.FleetManagement.ValueObjects;

/// <summary>
/// ValueObject that represents the Odomoter of a car, and includes the next service threshold -
/// i.e. the mileage/kilometrage at which the car should next be serviced.
/// 
/// Reading can be updated via a manual employee logging or automatic via telemetry (an OBS device)
/// </summary>
public class OdometerReading : ValueObject
{
    public OdometerUnits Units { get; init; }
    public float Value { get; init; }
    public float NextServiceThreshold { get; init; }

    public OdometerReading(OdometerUnits units, float value, float nextServiceThreshold)
    {
        Units = units;
        Value = value;
        NextServiceThreshold = nextServiceThreshold;
    }

    public static OdometerReading From(OdometerReading odometer, OdometerUnits? units = null, float? value = null, float? nextServiceThreshold = null)
    {
        return new OdometerReading(
            units ?? odometer.Units,
            value ?? odometer.Value,
            nextServiceThreshold ?? odometer.NextServiceThreshold
        );
    }

    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return Value;
        yield return Units;
        yield return NextServiceThreshold;
    }
}
