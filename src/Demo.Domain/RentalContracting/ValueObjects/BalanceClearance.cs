using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.RentalContracting.ValueObjects;

/// <summary>
/// A Value Object representing a DomainPolicy/DomainObject used by
/// <see cref="Rental.Close(BalanceClearance)"/> to enable/disable
/// the closure of a rental.
/// </summary>
public class BalanceClearance : ValueObject
{
    public bool Approved { get; init; }

    public BalanceClearance(bool isCleared)
    {
        Approved = isCleared;
    }

    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return Approved;
    }
}
