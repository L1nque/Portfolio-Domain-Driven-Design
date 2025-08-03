using Demo.Domain.RentalContracting.ValueObjects;
using Demo.SharedKernel.Core.Abstractions;

namespace Demo.Domain.RentalContracting.Interfaces;

/// <summary>
/// This is just a wrapper around <see cref="IRepository{TAggregate, TId}"/>
/// to simplify referencing the rental repository.
/// 
/// Instead of constantly doing: IRepository<Rental, RentalId>
/// Just do: IRentalRepository 
/// </summary>
public interface IRentalRepository : IRepository<Rental, RentalId>;