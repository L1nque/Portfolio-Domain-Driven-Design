using Demo.Domain.FleetManagement.ValueObjects;
using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.FleetManagement.Events;

public class CarServiceCompletedEvent : DomainEvent
{
    public CarServiceCompletedEvent(CarId id, ServiceLogId serviceLogId)
    {
        Id = id;
        ServiceLogId = serviceLogId;
    }

    public CarId Id { get; init; }
    public ServiceLogId ServiceLogId { get; init; }
}