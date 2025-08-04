using Demo.Domain.FleetManagement.ValueObjects;
using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.FleetManagement.Events;

public class CarOdometerUpdatedEvent : DomainEvent
{
    public CarId CarId { get; init; }
    public float Value { get; init; }

    public CarOdometerUpdatedEvent(CarId id, float value)
    {
        CarId = id;
        Value = value;
    }
}