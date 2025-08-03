using Demo.SharedKernel.Exceptions;

namespace Demo.Domain.RentalContracting.Exceptions;

public class OutstandingBalanceException : DomainException
{
    public OutstandingBalanceException(string message) : base(message)
    {
    }
}
