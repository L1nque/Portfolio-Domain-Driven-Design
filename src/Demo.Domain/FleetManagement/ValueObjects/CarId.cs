using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.FleetManagement.ValueObjects;

/// <summary>
/// The <see cref="StronglyTypedId{TId}"/>
/// for a <see cref="Car"/> in the Fleet Management context.
/// </summary>
public class CarId : StronglyTypedId<CarId>
{
    public CarId(Guid value) : base(value) { }
    public static CarId Create(Guid value) => new CarId(value);
}