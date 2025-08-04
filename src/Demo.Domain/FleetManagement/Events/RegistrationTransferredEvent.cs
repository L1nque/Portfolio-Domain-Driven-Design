using Demo.Domain.FleetManagement.ValueObjects;
using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.FleetManagement.Events;

public class RegistrationTransferredEvent : DomainEvent
{
    public RegistrationTransferredEvent(RegistrationId registrationId, CarId carId)
    {
        RegistrationId = registrationId;
        CarId = carId;
    }

    public RegistrationId RegistrationId { get; set; }
    public CarId CarId { get; set; }
}