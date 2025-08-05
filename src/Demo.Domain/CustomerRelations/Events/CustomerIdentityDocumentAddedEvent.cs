using Demo.Domain.CustomerRelations.Entities;
using Demo.Domain.CustomerRelations.ValueObjects;
using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.CustomerRelations.Events;

public class CustomerIdentityDocumentAddedEvent : DomainEvent
{
    public CustomerIdentityDocumentAddedEvent(CustomerId customerId, IdentityDocument document)
    {
        CustomerId = customerId;
        Document = document;
    }

    public CustomerId CustomerId { get; set; }
    public IdentityDocument Document { get; set; }
}