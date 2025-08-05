using Demo.Domain.CustomerRelations.Enums;
using Demo.Domain.CustomerRelations.ValueObjects;
using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.CustomerRelations.Entities;

public class IdentityDocument : Entity<IdentityDocumentId>
{
    public IdentityDocumentType Type { get; private set; }
    public string Number { get; private set; }
    public string IssuingAuthority { get; private set; }
    public DateOnly IssueDate { get; private set; }
    public DateOnly ExpiryDate { get; private set; }
    public IdentityDocumentVerificationStatus Status { get; private set; }

    public IdentityDocument(IdentityDocumentType type, string number, string issuingAuthority, DateOnly issueDate, DateOnly expiryDate)
    {
        Type = type;
        Number = number;
        IssuingAuthority = issuingAuthority;
        IssueDate = issueDate;
        ExpiryDate = expiryDate;
        Status = IdentityDocumentVerificationStatus.Pending;
    }

    public void MarkAsVerified()
    {
        Status = IdentityDocumentVerificationStatus.Verified;
    }

    public void MarkAsFailedVerification()
    {
        Status = IdentityDocumentVerificationStatus.Failed;
    }

    public bool IsExpiredAsOf(DateOnly asOfDate)
    {
        return ExpiryDate < asOfDate;
    }
}