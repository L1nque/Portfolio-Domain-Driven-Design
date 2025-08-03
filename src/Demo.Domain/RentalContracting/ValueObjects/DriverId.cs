using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.RentalContracting.ValueObjects;


/// <summary>
/// The <see cref="StronglyTypedId{TId}"/>
/// for a <see cref="Entities.Driver"/> in the RentalContracting context.
/// </summary>
public class DriverId : StronglyTypedId<DriverId>
{
    public DriverId(Guid value) : base(value) { }
    public static DriverId Create(Guid value) => new DriverId(value);
}