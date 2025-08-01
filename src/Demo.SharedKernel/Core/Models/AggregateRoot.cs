using Demo.SharedKernel.Core.Abstractions;

namespace Demo.SharedKernel.Core.Models;

/// <summary>
/// Abstract base class for implementing the Aggregate Root pattern.
/// An aggregate root is an entity that serves as the entry point for operations
/// on its cluster of associated objects (the aggregate).
/// </summary>
/// <typeparam name="TId">The type of the strongly-typed ID for the aggregate root.</typeparam>
public abstract class AggregateRoot<TId> : Entity<TId> where TId : StronglyTypedId<TId>
{
    private readonly List<IDomainEvent> _domainEvents = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateRoot{TId}"/> class with a specific ID.
    /// </summary>
    /// <param name="id">The unique identifier for the aggregate root.</param>
    protected AggregateRoot(TId id) : base(id) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateRoot{TId}"/> class.
    /// A new ID will be generated.
    /// </summary>
    protected AggregateRoot() : base() { }


    /// <summary>
    /// Gets the collection of domain events that have been raised by this aggregate root.
    /// These events are typically collected and dispatched by the application layer
    /// after the aggregate's state has been successfully persisted.
    /// </summary>
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// Adds a domain event to the internal collection.
    /// </summary>
    /// <param name="domainEvent">The domain event to add.</param>
    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    /// <summary>
    /// Removes a domain event from the internal collection.
    /// </summary>
    /// <param name="domainEvent">The domain event to remove.</param>
    protected void RemoveDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    /// <summary>
    /// Clears all domain events from the internal collection.
    /// This is typically called after the events have been successfully published.
    /// </summary>
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}