using Demo.Domain.FleetManagement.ValueObjects;
using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.FleetManagement.Events;

public class RegistrationSuspendedEvent : DomainEvent
{
    public RegistrationSuspendedEvent(RegistrationId registrationId, CarId carId)
    {
        RegistrationId = registrationId;
        CarId = carId;
    }

    public RegistrationId RegistrationId { get; init; }
    public CarId CarId { get; init; }
}