using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.FleetManagement.ValueObjects;

/// <summary>
/// The <see cref="StronglyTypedId{TId}"/>
/// for a <see cref="Registration"/> in the Fleet Management context.
/// </summary>
public class RegistrationId : StronglyTypedId<RegistrationId>
{
    public RegistrationId(Guid value) : base(value) { }
    public static RegistrationId Create(Guid value) => new RegistrationId(value);
}