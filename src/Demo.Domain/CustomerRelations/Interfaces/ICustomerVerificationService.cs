using Demo.Domain.CustomerRelations.ValueObjects;

namespace Demo.Domain.CustomerRelations.Interfaces;

public interface ICustomerVerificationService
{
    VerificationResult Verify(Customer customer);
}