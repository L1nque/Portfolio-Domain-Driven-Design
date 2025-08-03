using Demo.Domain.RentalContracting.ValueObjects;
using Demo.SharedKernel.Exceptions;

namespace Demo.Domain.RentalContracting.Exceptions;

public class DuplicateAddonException : DomainException
{
    public DuplicateAddonException(SelectedAddonId id) : base($"Addon {id} already exists")
    {
    }
}
