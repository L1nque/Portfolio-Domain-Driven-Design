using Demo.Domain.CustomerRelations.ValueObjects;
using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.CustomerRelations.Events;

public class CustomerRiskProfileUpdatedEvent : DomainEvent
{
    public CustomerRiskProfileUpdatedEvent(CustomerId customerId, RiskProfile riskProfile)
    {
        CustomerId = customerId;
        RiskProfile = riskProfile;
    }

    public CustomerId CustomerId { get; set; }
    public RiskProfile RiskProfile { get; set; }
}