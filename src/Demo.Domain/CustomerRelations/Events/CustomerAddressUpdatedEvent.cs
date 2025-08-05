using Demo.Domain.CustomerRelations.ValueObjects;
using Demo.SharedKernel.Core.Models;
using Demo.SharedKernel.Types;

namespace Demo.Domain.CustomerRelations.Events;

public class CustomerAddressUpdatedEvent : DomainEvent
{
    public CustomerAddressUpdatedEvent(CustomerId customerId, Address newAddress)
    {
        CustomerId = customerId;
        NewAddress = newAddress;
    }

    public CustomerId CustomerId { get; set; }
    public Address NewAddress { get; set; }
}