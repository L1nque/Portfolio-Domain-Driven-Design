using Demo.SharedKernel.Core.Abstractions;

namespace Demo.SharedKernel.Core.Models;

/// <summary>
/// Abstract base class for domain events.
/// Provides a common structure including a unique ID.
/// </summary>
public abstract class DomainEvent : IDomainEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DomainEvent"/> class.
    /// Sets the EventId to a new Guid and OccurredOn to the current UTC time.
    /// </summary>
    protected DomainEvent()
    {
        EventId = Guid.NewGuid();
    }

    /// <summary>
    /// Gets the unique identifier for the event.
    /// </summary>
    public Guid EventId { get; }
}