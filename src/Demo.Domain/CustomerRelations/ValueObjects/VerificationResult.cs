using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.CustomerRelations.ValueObjects;

public class VerificationResult : ValueObject
{
    public bool IsVerified { get; }

    public VerificationResult(bool isVerified)
    {
        IsVerified = isVerified;
    }

    public static VerificationResult Verified => new VerificationResult(true);
    public static VerificationResult Failed => new VerificationResult(false);

    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return IsVerified;
    }
}