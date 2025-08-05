using Demo.Domain.CustomerRelations.Enums;
using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.CustomerRelations.ValueObjects;

/// <summary>
/// Risk & Compliance BC has the logic that calculates the RiskLevel.
/// The Customer only holds a snapshot of the risk profile and does not "know"
/// how to calculate risk level.
/// 
/// Risk level is for e.g. calculated by looking up the Customers traffic history,
/// getting the total number of fines, averages, time between fines, etc.
/// 
/// Again, the complexity involved here is just for demonstration purposes.
/// </summary>
public class RiskProfile : ValueObject
{
    public RiskLevel RiskLevel { get; }

    public RiskProfile(RiskLevel riskLevel)
    {
        RiskLevel = riskLevel;
    }

    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return RiskLevel;
    }
}