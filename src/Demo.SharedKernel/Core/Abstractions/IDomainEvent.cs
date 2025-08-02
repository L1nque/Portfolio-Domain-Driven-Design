using MediatR;

namespace Demo.SharedKernel.Core.Abstractions;

/// <summary>
/// Represents a domain event, a lightweight notification indicating that something of importance
/// has happened within the domain. Domain events are used to achieve eventual consistency
/// and trigger side effects within the same bounded context.
/// </summary>
public interface IDomainEvent : INotification
{
    /// <summary>
    /// Gets the unique identifier for the event.
    /// </summary>
    Guid EventId { get; }
}