using Demo.Domain.FleetManagement.Enums;
using Demo.Domain.FleetManagement.ValueObjects;
using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.FleetManagement.Events;

public class CarStatusChangedEvent : DomainEvent
{
    public CarStatusChangedEvent(CarId id, CarStatus status)
    {
        Id = id;
        Status = status;
    }

    public CarId Id { get; init; }
    public CarStatus Status { get; init; }
}