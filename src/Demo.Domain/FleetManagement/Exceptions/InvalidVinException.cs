using Demo.SharedKernel.Exceptions;

namespace Demo.Domain.FleetManagement.Exceptions;

public class InvalidVinException : DomainException
{
    public InvalidVinException(int length)
        : base($"Vin numbers are exactly 17 characters, but a string of length {length} was provided")
    {
    }

    public InvalidVinException(string message) : base(message)
    {

    }
}
