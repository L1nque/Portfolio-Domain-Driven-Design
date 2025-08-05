using Demo.Domain.CustomerRelations.ValueObjects;
using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.CustomerRelations.Events;

public class CustomerVerifiedEvent : DomainEvent
{
    public CustomerId CustomerId { get; set; }

    public CustomerVerifiedEvent(CustomerId customerId)
    {
        CustomerId = customerId;
    }
}