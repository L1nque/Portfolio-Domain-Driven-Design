using Demo.Domain.FleetManagement.ValueObjects;
using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.FleetManagement.Events;

public class RegistrationExpiredEvent : DomainEvent
{
    public RegistrationId Id { get; set; }

    public RegistrationExpiredEvent(RegistrationId id)
    {
        Id = id;
    }
}