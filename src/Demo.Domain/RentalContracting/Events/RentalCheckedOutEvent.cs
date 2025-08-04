using Demo.Domain.RentalContracting.Entities;
using Demo.Domain.RentalContracting.ValueObjects;
using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.RentalContracting.Events;

public class RentalCheckedOutEvent : DomainEvent
{
    public DateTimeOffset Date { get; init; }
    public RentalId RentalId { get; init; }
    public RentalCarId CarId { get; init; }
    public DriverId DriverId { get; init; }
    public float OdomoterValue { get; init; }
    public float FuelLevel { get; init; }

    public RentalCheckedOutEvent(RentalId rentalId, RentalCarId carId, DriverId driverId, float odomoterValue, float fuelLevel)
    {
        Date = DateTimeOffset.UtcNow;
        RentalId = rentalId;
        CarId = carId;
        DriverId = driverId;
        OdomoterValue = odomoterValue;
        FuelLevel = fuelLevel;
    }
}