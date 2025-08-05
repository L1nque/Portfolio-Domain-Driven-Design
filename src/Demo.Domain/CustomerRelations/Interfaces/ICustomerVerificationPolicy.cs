namespace Demo.Domain.CustomerRelations.Interfaces;

public interface ICustomerVerificationPolicy
{
    bool Verify(Customer customer);
}