using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.RentalContracting.ValueObjects;


/// <summary>
/// The <see cref="StronglyTypedId{TId}"/>
/// for a <see cref="Entities.SelectedAddon"/> in the RentalContracting context.
/// </summary>
public class SelectedAddonId : StronglyTypedId<SelectedAddonId>
{
    public SelectedAddonId(Guid value) : base(value) { }
    public static SelectedAddonId Create(Guid value) => new SelectedAddonId(value);

}