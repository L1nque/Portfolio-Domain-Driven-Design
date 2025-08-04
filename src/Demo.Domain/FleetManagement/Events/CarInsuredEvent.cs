using Demo.Domain.FleetManagement.ValueObjects;
using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.FleetManagement.Events;

public class CarInsuredEvent : DomainEvent
{
    public CarId CarId { get; init; }
    public DateTime ExpiryDate { get; init; }

    public CarInsuredEvent(CarId id, DateTime expiryDate)
    {
        CarId = id;
        ExpiryDate = expiryDate;
    }
}