using Demo.Domain.CustomerRelations.ValueObjects;
using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.CustomerRelations.Events;

public class CustomerContactInformationUpdatedEvent : DomainEvent
{
    public CustomerContactInformationUpdatedEvent(CustomerId customerId, ContactInformation newContactInformation)
    {
        CustomerId = customerId;
        NewContactInformation = newContactInformation;
    }

    public CustomerId CustomerId { get; set; }
    public ContactInformation NewContactInformation { get; set; }
}