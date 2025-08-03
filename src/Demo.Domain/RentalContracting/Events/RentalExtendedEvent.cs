using Demo.Domain.RentalContracting.ValueObjects;
using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.RentalContracting.Events;

public class RentalExtendedEvent : DomainEvent
{
    public RentalExtendedEvent(RentalId rentalId, RentalPeriod period)
    {
        RentalId = rentalId;
        Period = period;
    }

    public RentalId RentalId { get; init; }
    public RentalPeriod Period { get; init; }

}