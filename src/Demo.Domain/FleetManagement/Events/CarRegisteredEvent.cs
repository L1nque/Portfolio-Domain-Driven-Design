using Demo.Domain.FleetManagement.ValueObjects;
using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.FleetManagement.Events;

public class CarRegisteredEvent : DomainEvent
{
    public CarId CarId { get; init; }
    public RegistrationId RegistrationId { get; init; }

    public CarRegisteredEvent(CarId carId, RegistrationId registrationId)
    {
        CarId = carId;
        RegistrationId = registrationId;
    }
}