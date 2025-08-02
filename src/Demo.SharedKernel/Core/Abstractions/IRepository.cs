using Demo.SharedKernel.Core.Models;

namespace Demo.SharedKernel.Core.Abstractions;

/// <summary>
/// Generic interface for repository pattern, providing basic persistence operations
/// for aggregate roots in the domain model.
/// </summary>
/// <typeparam name="TAggregate"><see cref="AggregateRoot{TId}"/>  managed by this repository.</typeparam>
/// <typeparam name="TId"><see cref="StronglyTypedId{TId}"/> used by the aggregate root.</typeparam>
public interface IRepository<TAggregate, TId>
    where TAggregate : AggregateRoot<TId>
    where TId : StronglyTypedId<TId>
{
    /// <summary>
    /// Retrieves an aggregate by its StronglyTypedId.
    /// </summary>
    /// <param name="id">The identifier of the aggregate.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>The aggregate root if found; otherwise null.</returns>
    Task<TAggregate?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new aggregate root to the repository.
    /// This typically marks the aggregate for insertion into the underlying data store.
    /// </summary>
    /// <param name="aggregate">The new aggregate instance to be persisted.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    Task AddAsync(TAggregate aggregate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing aggregate in the repository.
    /// This operation may be a no-op in some persistence strategies (e.g., event sourcing).
    /// </summary>
    /// <param name="aggregate">The aggregate instance with updated state.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    Task UpdateAsync(TAggregate aggregate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an aggregate from the repository.
    /// This may be a soft delete, hard delete, or raise a domain event depending on the implementation.
    /// </summary>
    /// <param name="aggregate">The aggregate to be deleted.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    Task DeleteAsync(TAggregate aggregate, CancellationToken cancellationToken = default);
}
