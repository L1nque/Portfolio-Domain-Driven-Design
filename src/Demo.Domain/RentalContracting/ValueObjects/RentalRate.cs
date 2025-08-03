using Demo.SharedKernel.Core.Models;
using Demo.SharedKernel.Types;

namespace Demo.Domain.RentalContracting.ValueObjects;


/// <summary>
/// ValueObject that encapsulates the daily/weekly/monthly rates for a rental.
/// </summary>
public class RentalRate : ValueObject
{
    public Money Daily { get; init; }
    public Money Weekly { get; init; }
    public Money Monthly { get; init; }

    public RentalRate(Money daily, Money weekly, Money monthly)
    {
        Daily = daily;
        Weekly = weekly;
        Monthly = monthly;
    }

    /// <summary>
    /// If a rental has exceed 7 days,
    /// then apply the weekly rate automatically.
    /// If a rental has exceeded 30 days,
    /// then apply the monthly rate.
    /// Otherwise, apply the daily rate. 
    /// </summary>
    /// <param name="period">The rental period</param>
    /// <returns></returns>
    public Money GetRate(RentalPeriod period)
    {
        if (period.Duration() < 7)
            return Daily;

        if (period.Duration() < 30)
            return Weekly;

        return Monthly;
    }

    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return Daily;
        yield return Weekly;
        yield return Monthly;
    }
}
