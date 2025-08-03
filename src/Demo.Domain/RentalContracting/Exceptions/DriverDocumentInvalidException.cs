using Demo.SharedKernel.Exceptions;

namespace Demo.Domain.RentalContracting.Exceptions;

public class DriverDocumentInvalidException : DomainException
{
    public DriverDocumentInvalidException(string message) : base(message)
    {
    }
}
