using Demo.Domain.CustomerRelations.ValueObjects;
using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.CustomerRelations.Events;

public class CustomerBehaviorProfileUpdatedEvent : DomainEvent
{
    public CustomerBehaviorProfileUpdatedEvent(CustomerId customerId, BehaviorProfile behaviorProfile)
    {
        CustomerId = customerId;
        BehaviorProfile = behaviorProfile;
    }

    public CustomerId CustomerId { get; set; }
    public BehaviorProfile BehaviorProfile { get; set; }
}