using Demo.SharedKernel.Exceptions;

namespace Demo.Domain.FleetManagement.Exceptions;

public class InvalidRegistrationStateException : DomainException
{
    public InvalidRegistrationStateException(string message) : base(message)
    {
    }
}