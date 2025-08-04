using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.FleetManagement.ValueObjects;

/// <summary>
/// This is a property of the <see cref="Car"/> that is necessary for compliance
/// and operation.
/// 
/// Ideally we would have an 'Insurance Management' subdomain which this VO contextually
/// maps to.
/// </summary>
public class InsuranceCompliance : ValueObject
{
    public Guid PolicyId { get; init; }
    public string PolicyNumber { get; init; }
    public DateTime ExpirationDate { get; init; }

    public InsuranceCompliance(Guid policyId, string policyNumber, DateTime expirationDate)
    {
        PolicyId = policyId;
        PolicyNumber = policyNumber;
        ExpirationDate = expirationDate;
    }

    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return PolicyId;
        yield return PolicyNumber;
        yield return ExpirationDate;
    }
}
