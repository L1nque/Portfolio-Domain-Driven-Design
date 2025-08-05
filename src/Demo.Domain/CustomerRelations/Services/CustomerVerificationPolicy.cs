using Demo.Domain.CustomerRelations.Enums;
using Demo.Domain.CustomerRelations.Interfaces;
using Demo.SharedKernel.Core.Abstractions;

namespace Demo.Domain.CustomerRelations.Services;

/// <summary>
/// "Policies" are in their own way, domain services as well.
/// - The service defines a business rule or constraint (binary yes/no or decision based)
/// - It represents something that may vary by context (tenant, country, regulation, product/vehicle).
/// - The ubiquitous language speaks in terms of “policies” - Verification Policy, Refund Policy, 
///   Discount Policy, Compliance Policy.
/// </summary>
public class CustomerVerificationPolicy : ICustomerVerificationPolicy
{
    private readonly ITimeProvider _timeProvider;

    public CustomerVerificationPolicy(ITimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }

    /// <summary>
    /// In this case, a customer can only be verified if:
    /// - They have at least 2 verified documents
    /// - The verified documents are valid (not expired)
    /// - One of the verified, valid documents is a driver's license 
    /// </summary>
    /// <param name="customer"></param>
    /// <returns></returns>
    public bool Verify(Customer customer)
    {
        var today = DateOnly.FromDateTime(_timeProvider.UtcNow.Date);

        var validDocuments = customer.Documents
            .Where(x => x.Status == IdentityDocumentVerificationStatus.Verified && x.ExpiryDate >= today);


        if (validDocuments.Count() >= 2 && validDocuments.Any(x => x.Type == IdentityDocumentType.DriversLicense))
        {
            return true;
        }

        return false;
    }
}