using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.RentalContracting.ValueObjects;

/// <summary>
/// The <see cref="StronglyTypedId{TId}"/>
/// for a <see cref="Entities.RentalCar"/> in the RentalContracting context.
/// </summary>
public class RentalCarId : StronglyTypedId<RentalCarId>
{
    public RentalCarId(Guid value) : base(value) { }
    public static RentalCarId Create(Guid value) => new RentalCarId(value);
}