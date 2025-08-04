using Demo.Domain.FleetManagement.Enums;
using Demo.Domain.FleetManagement.Events;
using Demo.Domain.FleetManagement.Exceptions;
using Demo.Domain.FleetManagement.ValueObjects;
using Demo.SharedKernel.Core.Abstractions;
using Demo.SharedKernel.Core.Models;
using Demo.SharedKernel.Types;

namespace Demo.Domain.FleetManagement;

/// <summary>
/// The <see cref="Registration"/> acts as one of two AggregateRoots
/// within the Fleet Management bounded context, and shares the same
/// Ubiquitous language.
/// 
/// <para>
///     It Represents the legal authorization for a vehicle to operate
///     on public roads and is issued by a governmental authority,
///     acting as a "regulatory" contract.
/// </para>
/// </summary>
public class Registration : AggregateRoot<RegistrationId>
{
    /// <summary>
    /// Holds a reference to the registered car
    /// </summary>
    public CarId CarId { get; private set; } = null!;

    /// <summary>
    /// Path to S3 storage or url of registration on gov site
    /// </summary>
    public Uri Url { get; private set; } = null!;

    /// <summary>
    /// The cost of the registration
    /// </summary>
    public Money Cost { get; private set; } = null!;

    /// <summary>
    /// After expiration, there generally is a grace period
    /// </summary>
    public int GracePeriodInDays { get; private set; }

    /// <summary>
    /// A registration can transition between different states     
    /// </summary>
    public RegistrationStatus Status { get; private set; }

    /// <summary>
    /// The period during which the registration is valid,
    /// excludes grace period
    /// </summary>
    public DateRange EffectivePeriod { get; private set; } = null!;

    /// <summary>
    /// The plates of the registration. While plates are usually
    /// stuck on a car, they are actually part of its registration.
    /// </summary>
    public CarPlates Plates { get; private set; } = null!;

    /// <summary>
    /// Instantiates a Registration using a factory method for a change.
    /// </summary>
    public static Registration Issue(CarPlates plates, CarId carId, DateRange effectivePeriod, Uri url, Money cost, int gracePeriodInDays)
    {
        return new Registration
        {
            Plates = plates,
            CarId = carId,
            Url = url,
            Cost = cost,
            EffectivePeriod = effectivePeriod,
            GracePeriodInDays = gracePeriodInDays,
        };
    }


    /// <summary>
    /// Marks a registration as suspended, perhaps due to a 
    /// jurisdiction violation
    /// </summary>
    public void Suspend()
    {
        Status = RegistrationStatus.Suspeneded;
        AddDomainEvent(new RegistrationSuspendedEvent(Id, CarId));
    }

    /// <summary>
    /// Reinstates an expired or suspended registration
    /// </summary>
    /// <exception cref="InvalidRegistrationStateException"></exception>
    public void Reinstate()
    {
        if (Status != RegistrationStatus.Expired && Status != RegistrationStatus.Suspeneded)
        {
            throw new InvalidRegistrationStateException("Cannot reinstate a registration that is not expired/suspended.");
        }

        Status = RegistrationStatus.Active;
        AddDomainEvent(new RegistrationReinstatedEvent(Id));
    }


    /// <summary>
    /// Marks a registration as expired. This is only possible
    /// if the current date is not before the expiration + grace period.
    /// 
    /// Time is calculated using an abstraction for testability and so that
    /// the domain is not reliant on system time.
    /// </summary>
    /// <param name="timeProvider"><see cref="ITimeProvider"/></param>
    /// <exception cref="RegistrationExpirationException"></exception>
    public void Expire(ITimeProvider timeProvider)
    {
        if (timeProvider.UtcNow < EffectivePeriod.End.AddDays(GracePeriodInDays))
        {
            throw new RegistrationExpirationException("Expiration date has not been crossed.");
        }

        Status = RegistrationStatus.Expired;
        AddDomainEvent(new RegistrationExpiredEvent(Id));
    }

    /// <summary>
    /// Transfers the registration to a new car. Cannot be done
    /// if the registration is suspended.
    /// </summary>
    /// <param name="carId">The car to transfer it to</param>
    /// <exception cref="InvalidRegistrationStateException"></exception>
    public void Transfer(CarId carId)
    {
        if (Status == RegistrationStatus.Suspeneded)
        {
            throw new InvalidRegistrationStateException("Cannot transfer registration due to suspension.");
        }

        CarId = carId;
        AddDomainEvent(new RegistrationTransferredEvent(Id, carId));
    }
}