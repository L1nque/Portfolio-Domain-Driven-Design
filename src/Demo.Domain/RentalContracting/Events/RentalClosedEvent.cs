using Demo.Domain.RentalContracting.ValueObjects;
using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.RentalContracting.Events;

public class RentalClosedEvent : DomainEvent
{
    public RentalClosedEvent(RentalId rentalId)
    {
        RentalId = rentalId;
    }

    public RentalId RentalId { get; init; }

}