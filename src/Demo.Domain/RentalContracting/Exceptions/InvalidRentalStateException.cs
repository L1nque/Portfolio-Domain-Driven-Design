using Demo.SharedKernel.Exceptions;

namespace Demo.Domain.RentalContracting.Exceptions;

public class InvalidRentalStateException : DomainException
{
    public InvalidRentalStateException(string message) : base(message)
    {
    }
}
