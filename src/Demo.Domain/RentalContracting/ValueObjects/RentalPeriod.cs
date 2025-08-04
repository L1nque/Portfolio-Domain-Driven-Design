using Demo.SharedKernel.Core.Abstractions;
using Demo.SharedKernel.Core.Models;
using Demo.SharedKernel.Types;

namespace Demo.Domain.RentalContracting.ValueObjects;

/// <summary>
/// ValueObject that encapsulates the duration of a rental
/// </summary>
public class RentalPeriod : ValueObject
{
    public static readonly int AdditionalDayThresholdHours = 3;
    public DateRange Dates { get; init; }

    public RentalPeriod(DateRange dates)
    {
        Dates = dates;
    }

    /// <summary>
    ///     <para>
    ///         The intention is to calculate the duration up until today.
    ///         We could use DateTime.UtcNow, however, mixing system time with a Value Object is risky. 
    ///         Also, it would leak infrastructure concerns into the domain layer.
    ///     </para>
    /// 
    ///     <para>
    ///         We could consider moving it to a domain service or an external calculator, however,
    ///         I just wanted to make use of <see cref="ITimeProvider"/> somewhere, to show a use-case
    ///         for it.
    ///     </para>
    /// 
    ///     <para>
    ///         Additionally, take the number of whole days between the dates, 
    ///         and if the time difference (on the partial day) is >= <see cref="AdditionalDayThresholdHours"/>, 
    ///         count it as an extra rental day.
    ///     </para>
    /// </summary>
    /// <param name="timeProvider"></param>
    public int TrueRentalDuration(ITimeProvider timeProvider)
    {
        var duration = (timeProvider.UtcNow - Dates.Start).Days;
        var timeDifference = timeProvider.UtcNow.TimeOfDay - Dates.Start.TimeOfDay;

        if (timeDifference.TotalHours >= AdditionalDayThresholdHours)
        {
            duration++;
        }

        return duration;
    }

    public static RentalPeriod Extend(RentalPeriod current, DateTimeOffset newCheckoutDate)
    {
        if (newCheckoutDate <= current.Dates.End || newCheckoutDate < current.Dates.Start)
            throw new InvalidOperationException();

        return new RentalPeriod(new DateRange(current.Dates.Start, newCheckoutDate));
    }

    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return Dates;
    }
}
