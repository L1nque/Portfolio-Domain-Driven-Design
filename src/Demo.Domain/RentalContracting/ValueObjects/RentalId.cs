using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.RentalContracting.ValueObjects;

/// <summary>
/// The <see cref="StronglyTypedId{TId}"/>
/// for a <see cref="Rental"/> in the RentalContracting context.
/// </summary>
public class RentalId : StronglyTypedId<RentalId>
{
    public RentalId(Guid value) : base(value) { }
    public static RentalId Create(Guid value) => new RentalId(value);
}