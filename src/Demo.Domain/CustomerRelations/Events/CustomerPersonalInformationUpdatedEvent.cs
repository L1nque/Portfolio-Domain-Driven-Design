using Demo.Domain.CustomerRelations.ValueObjects;
using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.CustomerRelations.Events;

public class CustomerPersonalInformationUpdatedEvent : DomainEvent
{
    public CustomerPersonalInformationUpdatedEvent(CustomerId customerId, PersonalInformation newInformation)
    {
        CustomerId = customerId;
        NewInformation = newInformation;
    }

    public CustomerId CustomerId { get; set; }
    public PersonalInformation NewInformation { get; set; }
}