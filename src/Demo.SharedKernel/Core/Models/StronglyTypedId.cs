using System.Diagnostics;

namespace Demo.SharedKernel.Core.Models;

/// <summary>
/// Abstract base class for creating strongly-typed IDs.
/// This helps prevent accidental mixing of different ID types (e.g., CustomerId vs OrderId).
/// </summary>
/// <typeparam name="TId">The type of the strongly-typed ID (e.g., CustomerId).</typeparam>
[DebuggerDisplay("{Value}")] // for convenient debugging
public abstract class StronglyTypedId<TId> : ValueObject where TId : StronglyTypedId<TId>
{
    /// <summary>
    /// Gets the underlying Guid value of the ID.
    /// </summary>
    public Guid Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="StronglyTypedId{TId}"/> class.
    /// </summary>
    /// <param name="value">The Guid value for the ID. Must not be empty.</param>
    /// <exception cref="ArgumentException">Thrown if the provided value is Guid.Empty.</exception>
    protected StronglyTypedId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("ID cannot be empty.", nameof(value));
        }
        Value = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StronglyTypedId{TId}"/> class with a new random Guid.
    /// </summary>
    protected StronglyTypedId() : this(Guid.NewGuid()) { }

    /// <summary>
    /// Gets the atomic values for equality comparison.
    /// </summary>
    /// <returns>An enumerable containing the Guid value.</returns>
    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return Value;
    }

    /// <summary>
    /// Returns a string representation of the ID's value.
    /// </summary>
    /// <returns>The string representation of the Guid value.</returns>
    public override string ToString() => Value.ToString();

    /// <summary>
    /// Implicitly converts a strongly-typed ID to its underlying Guid value.
    /// </summary>
    /// <param name="id">The strongly-typed ID.</param>
    public static implicit operator Guid(StronglyTypedId<TId> id) => id.Value;

    // Note: Implicit conversion from Guid to StronglyTypedId<TId> is not possible
    // due to the abstract nature. Concrete implementations must provide a static factory method.
    // Example in a concrete class:
    // public static CustomerId Create(Guid value) => new CustomerId(value);
}