using Demo.Domain.RentalContracting.Entities;
using Demo.Domain.RentalContracting.ValueObjects;
using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.RentalContracting.Events;

public class RentalCheckedInEvent : DomainEvent
{
    public RentalPeriod Period { get; init; }
    public RentalId RentalId { get; init; }
    public RentalCarId CarId { get; init; }
    public DriverId DriverId { get; init; }

    public RentalCheckedInEvent(RentalId rentalId, RentalCarId carId, DriverId driverId, RentalPeriod period)
    {
        Period = period;
        RentalId = rentalId;
        CarId = carId;
        DriverId = driverId;
    }
}
