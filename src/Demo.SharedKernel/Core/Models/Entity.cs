namespace Demo.SharedKernel.Core.Models;

/// <summary>
/// Abstract base class for entities within the domain.
/// Provides a strongly-typed unique identifier and basic equality comparison logic based on that identifier.
/// </summary>
/// <typeparam name="TId">The type of the strongly-typed ID, derived from StronglyTypedId<TId>.</typeparam>
public abstract class Entity<TId> where TId : StronglyTypedId<TId>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{TId}"/> class with a specific ID.
    /// </summary>
    /// <param name="id">The unique identifier for the entity.</param>
    protected Entity(TId id)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{TId}"/> class.
    /// A new ID will be generated if the concrete StronglyTypedId implementation supports it (e.g., via parameterless constructor).
    /// </summary>
    /// <remarks>
    /// This constructor relies on the concrete TId having a parameterless constructor that generates a new ID.
    /// If TId does not have such a constructor, this will throw an exception at runtime.
    /// </remarks>
    protected Entity()
    {
        try
        {
            // This assumes TId has a parameterless constructor that generates a new ID.
            // This is a common pattern for strongly-typed IDs.
            Id = (TId)Activator.CreateInstance(typeof(TId))!;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"The type {typeof(TId).Name} must have a parameterless constructor to use this Entity constructor.", ex);
        }
    }


    /// <summary>
    /// Gets the strongly-typed unique identifier for the entity.
    /// </summary>
    public TId Id { get; protected set; }

    /// <summary>
    /// Determines whether the specified object is equal to the current entity.
    /// Two entities are considered equal if they are of the same type and have the same Id value.
    /// Transient entities (Id.Value == Guid.Empty) are only equal to themselves.
    /// </summary>
    /// <param name="obj">The object to compare with the current entity.</param>
    /// <returns>true if the specified object is equal to the current entity; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is not Entity<TId> other)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (GetType() != other.GetType())
            return false;

        // Handle transient entities if the underlying value is Guid.Empty
        // This check depends on the Id type having a Value property (common for Guid-based StronglyTypedId)
        // A more generic approach might require an IEquatable<TId> constraint or an IIdentity interface.
        if (Id.Value == Guid.Empty || other.Id.Value == Guid.Empty)
            return false;

        return Id.Equals(other.Id);
    }

    /// <summary>
    /// Serves as the default hash function.
    /// Returns the hash code of the entity's Id.
    /// </summary>
    /// <returns>A hash code for the current entity.</returns>
    public override int GetHashCode()
    {
        // Handle transient entities
        if (Id.Value == Guid.Empty)
            return base.GetHashCode();

        return Id.GetHashCode();
    }

    /// <summary>
    /// Compares two entities for equality.
    /// </summary>
    /// <param name="left">The first entity to compare.</param>
    /// <param name="right">The second entity to compare.</param>
    /// <returns>true if the entities are equal; otherwise, false.</returns>
    public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
    {
        if (ReferenceEquals(left, right))
            return true;

        if (left is null || right is null)
            return false;

        return left.Equals(right);
    }

    /// <summary>
    /// Compares two entities for inequality.
    /// </summary>
    /// <param name="left">The first entity to compare.</param>
    /// <param name="right">The second entity to compare.</param>
    /// <returns>true if the entities are not equal; otherwise, false.</returns>
    public static bool operator !=(Entity<TId>? left, Entity<TId>? right)
    {
        return !(left == right);
    }
}