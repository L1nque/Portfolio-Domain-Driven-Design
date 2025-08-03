using Demo.Domain.RentalContracting.ValueObjects;
using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.RentalContracting.Entities;

/// <summary>
/// The SelectedAddon entity represents an addon chosen for a specific rental.
/// It originates from the AddonServices bounded context.
/// It is part of the Rental aggregate, managed by the Rental aggregate root.
/// It holds information about the addon (quantity, if it enables a second driver).
/// 
/// This is a good example of an Anemic Domain model - it has no behavior and just holds data.
/// I made it as such on purpose to showcase an anemic domain model, however, if the intention
/// is for it to just be a "Data model", then its better to mark it as ValueObject.
/// </summary>
public class SelectedAddon : Entity<SelectedAddonId>
{
    /// <summary>
    /// Indicates whether the addon allows <see cref="Rental.AddSecondaryDriver(Driver)"/>
    /// to add a second driver.
    /// 
    /// There are only two (default) addons from the AddonServices that enable this:
    /// - AdditionalDriver
    /// - BabySeat (assumes two parents and therefore enables additional driver) 
    /// </summary>
    public bool EnablesAdditionalDriver { get; init; }

    /// <summary>
    /// The quanitty of the addon.
    /// </summary>
    public int Quantity { get; init; }

    public SelectedAddon(SelectedAddonId id, int quantity, bool enablesAdditionalDriver = false)
        : base(id)
    {
        Quantity = quantity;
        EnablesAdditionalDriver = enablesAdditionalDriver;
    }
}