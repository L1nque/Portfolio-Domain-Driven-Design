using Demo.SharedKernel.Core.Abstractions;

namespace Demo.SharedKernel.Core.Models;

/// <summary>
/// Abstract base class for domain events.
/// Provides a common structure including a unique ID and occurrence timestamp.
/// </summary>
public abstract class DomainEvent : IDomainEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DomainEvent"/> class.
    /// Sets the EventId to a new Guid and OccurredOn to the current UTC time.
    /// </summary>
    /// <param name="timeProvider">The time provider to get the occurrence timestamp.</param>
    protected DomainEvent(ITimeProvider timeProvider)
    {
        EventId = Guid.NewGuid();
        OccurredOn = timeProvider.UtcNow;
    }

    /// <summary>
    /// Gets the unique identifier for the event.
    /// </summary>
    public Guid EventId { get; }

    /// <summary>
    /// Gets the date and time when the event occurred (in UTC).
    /// </summary>
    public DateTimeOffset OccurredOn { get; }
}