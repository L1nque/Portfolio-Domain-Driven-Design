using Demo.SharedKernel.Core.Models;
using Demo.SharedKernel.Types;

namespace Demo.Domain.RentalContracting.ValueObjects;

/// <summary>
/// ValueObject that acts a snapshot of the agreement of the MileagePolicy.
/// </summary>
public class MileagePolicy : ValueObject
{
    public OdometerReading Allowance { get; init; }
    public Money ExcessCharge { get; init; }

    public MileagePolicy(OdometerReading allowance, Money excessCharge)
    {
        Allowance = allowance;
        ExcessCharge = excessCharge;
    }

    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return Allowance;
        yield return ExcessCharge;
    }
}
