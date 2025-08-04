using Demo.Domain.FleetManagement.ValueObjects;
using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.FleetManagement.Events;

public class RegistrationReinstatedEvent : DomainEvent
{
    public RegistrationReinstatedEvent(RegistrationId id)
    {
        Id = id;
    }

    public RegistrationId Id { get; init; }
}