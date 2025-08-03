using Demo.SharedKernel.Exceptions;

namespace Demo.Domain.RentalContracting.Exceptions;

public class MissingAddonException : DomainException
{
    public MissingAddonException(string message) : base(message)
    {
    }
}
