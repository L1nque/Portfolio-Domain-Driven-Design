using Demo.SharedKernel.Core.Abstractions;
using Demo.SharedKernel.Core.Models;

namespace Demo.Domain.RentalContracting.ValueObjects;

/// <summary>
/// ValueObject that encapsulates the duration of a rental
/// </summary>
public class RentalPeriod : ValueObject
{
    public DateTimeOffset CheckInDate { get; init; }
    public DateTimeOffset CheckOutDate { get; init; }

    public RentalPeriod(DateTimeOffset checkInDate, DateTimeOffset checkOutDate)
    {
        if (checkOutDate < CheckInDate)
        {
            throw new ArgumentOutOfRangeException("Check-out cannot be before Check-in.");
        }

        CheckInDate = checkInDate;
        CheckOutDate = checkOutDate;
    }


    /// <summary>
    /// Calculates duration based on the Check-in/Check-out dates
    /// </summary>
    public int Duration()
    {
        return (CheckOutDate - CheckInDate).Days;
    }


    /// <summary>
    /// The intention is to calculate the duration up until today.
    /// We could use DateTime.UtcNow, however, mixing system time with a Value Object is risky. 
    /// Also, it would leak infrastructure concerns into the domain layer.
    /// 
    /// We could consider moving it to a domain service or an external calculator, however,
    /// I just wanted to make use of <see cref="ITimeProvider"/> somewhere, to show a use-case
    /// for it. 
    /// </summary>
    /// <param name="timeProvider"></param>
    public int TrueDuration(ITimeProvider timeProvider)
    {
        return (timeProvider.UtcNow - CheckInDate).Days;
    }

    public static RentalPeriod Extend(RentalPeriod current, DateTimeOffset newCheckoutDate)
    {
        if (newCheckoutDate <= current.CheckOutDate || newCheckoutDate < current.CheckInDate)
            throw new InvalidOperationException();

        return new RentalPeriod(current.CheckInDate, newCheckoutDate);
    }

    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return CheckInDate;
        yield return CheckOutDate;
    }
}
