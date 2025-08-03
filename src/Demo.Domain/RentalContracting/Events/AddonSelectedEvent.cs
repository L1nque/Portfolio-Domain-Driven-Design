using Demo.Domain.RentalContracting.ValueObjects;
using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.RentalContracting.Events;

public class AddonSelectedEvent : DomainEvent
{
    public AddonSelectedEvent(RentalId rentalId, SelectedAddonId selectedAddonId, Guid addonId)
    {
        RentalId = rentalId;
        SelectedAddonId = selectedAddonId;
        AddonId = addonId;
    }

    public RentalId RentalId { get; init; }
    public SelectedAddonId SelectedAddonId { get; init; }
    public Guid AddonId { get; init; }

}