using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.FleetManagement.ValueObjects;

/// <summary>
/// The <see cref="StronglyTypedId{TId}"/>
/// for a <see cref="Entities.ServiceLog"/> in the Fleet Management context.
/// </summary>
public class ServiceLogId : StronglyTypedId<ServiceLogId>
{
    public ServiceLogId(Guid value) : base(value) { }
    public static ServiceLogId Create(Guid value) => new ServiceLogId(value);
}