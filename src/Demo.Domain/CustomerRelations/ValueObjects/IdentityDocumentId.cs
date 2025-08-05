using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.CustomerRelations.ValueObjects;

/// <summary>
/// The <see cref="StronglyTypedId{TId}"/>
/// for a <see cref="Customer"/> in the Customer Relations context.
/// </summary>
public class IdentityDocumentId : StronglyTypedId<IdentityDocumentId>
{
    public IdentityDocumentId(Guid value) : base(value) { }
    public static IdentityDocumentId Create(Guid value) => new IdentityDocumentId(value);
}