using Demo.Domain.RentalContracting.ValueObjects;
using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.RentalContracting.Events;

public class RentalCancelledEvent : DomainEvent
{
    public RentalId RentalId { get; init; }

    public RentalCancelledEvent(RentalId rentalId)
    {
        RentalId = rentalId;
    }
}