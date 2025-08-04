using Demo.Domain.FleetManagement.Enums;
using Demo.SharedKernel.Core.Models;
using Demo.SharedKernel.Types;

namespace Demo.Domain.FleetManagement.ValueObjects;

/// <summary>
/// VO representing damage to a <see cref="Car"/>
/// </summary>
public class Damage : ValueObject
{
    public DamageSeverity Severity { get; init; }
    public string Description { get; init; }
    public Money? CostOfRepair { get; init; }
    public bool Insured { get; init; }

    public Damage(DamageSeverity severity, string description, Money? estimatedCost, bool insured)
    {
        Severity = severity;
        Description = description;
        CostOfRepair = estimatedCost;
        Insured = insured;
    }

    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return Description;
        yield return CostOfRepair;
    }
}
