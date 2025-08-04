using Demo.Domain.FleetManagement.Enums;
using Demo.Domain.FleetManagement.ValueObjects;
using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.FleetManagement.Entities;

/// <summary>
/// This is an entity internal to the <see cref="Car"/> Aggregate.
/// The model seems a little bit anemic and we can argue that this should be
/// a ValueObject.
/// 
/// Based on the current design of this Entity, I have no justification as to
/// why I've made it an entity.
/// </summary>
public class ServiceLog : Entity<ServiceLogId>
{
    public ServiceType Type { get; init; }
    public float ServicedAtMileage { get; init; }
    public float NextServiceThreshold { get; init; }

    public ServiceLog(ServiceType type, float servicedAtMileage, float nextServiceThreshold)
    {
        Type = type;
        ServicedAtMileage = servicedAtMileage;
        NextServiceThreshold = nextServiceThreshold;
    }
}