using Demo.SharedKernel.Exceptions;

namespace Demo.Domain.FleetManagement.Exceptions;

public class RegistrationExpirationException : DomainException
{
    public RegistrationExpirationException(string message) : base(message)
    {
    }
}