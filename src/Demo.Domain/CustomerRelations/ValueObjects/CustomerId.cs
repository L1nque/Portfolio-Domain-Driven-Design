using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.CustomerRelations.ValueObjects;

/// <summary>
/// The <see cref="StronglyTypedId{TId}"/>
/// for a <see cref="Customer"/> in the Customer Relations context.
/// </summary>
public class CustomerId : StronglyTypedId<CustomerId>
{
    public CustomerId(Guid value) : base(value) { }
    public static CustomerId Create(Guid value) => new CustomerId(value);
}