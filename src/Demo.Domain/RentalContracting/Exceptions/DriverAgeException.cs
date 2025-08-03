using Demo.Domain.RentalContracting.Entities;
using Demo.SharedKernel.Exceptions;

namespace Demo.Domain.RentalContracting.Exceptions;

public class DriverAgeException : DomainException
{
    public DriverAgeException() : base($"Driver is younger than {Driver.MinimumAge}")
    {
    }
}
