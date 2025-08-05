using Demo.Domain.CustomerRelations.Interfaces;
using Demo.Domain.CustomerRelations.ValueObjects;

namespace Demo.Domain.CustomerRelations.Services;

/// <summary>
/// This might seem like an uneccessary extra step/wrapper around the <see cref="CustomerVerificationPolicy"/>,
/// as in this context its very simple, however, a policy defines the rule, and the service orchestrates the logic.
/// 
/// Assume we had multiple different policies, or policies were context dependent (country, tenant, etc.),
/// the service is what would orchestrate this.
/// </summary>
public class CustomerVerificationService : ICustomerVerificationService
{
    private readonly ICustomerVerificationPolicy _policy;

    public CustomerVerificationService(ICustomerVerificationPolicy policy)
    {
        _policy = policy;
    }

    /// <summary>
    /// This is very simple the way it is, and we can actually bypass the use of a service
    /// altogether to verify the customer. However, in a more complex scenario, where we need
    /// to run multiple check, maybe use multiple different policies, etc. and this is where
    /// a domain service like this would shine.
    /// </summary>
    /// <param name="customer"></param>
    /// <returns></returns>
    public VerificationResult Verify(Customer customer)
    {
        if (_policy.Verify(customer))
        {
            return VerificationResult.Verified;
        }

        return VerificationResult.Failed;
    }
}