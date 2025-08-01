namespace Demo.SharedKernel.Extensions;

/// <summary>
/// Provides extension methods for DateTime manipulation.
/// </summary>
public static class DateTimeExtensions
{
    /// <summary>
    /// Calculates the age of a person based on their date of birth.
    /// </summary>
    /// <param name="dateOfBirth">The date of birth.</param>
    /// <param name="asOf">The date to calculate the age as of. Defaults to the current UTC date.</param>
    /// <returns>The age in years.</returns>
    /// <exception cref="ArgumentException">Thrown if the date of birth is in the future relative to the 'asOf' date.</exception>
    public static int CalculateAge(this DateTime dateOfBirth, DateTime? asOf = null)
    {
        var referenceDate = asOf ?? DateTime.UtcNow;
        var age = referenceDate.Year - dateOfBirth.Year;

        // Adjust age if the birthday hasn't occurred yet this year
        if (referenceDate < dateOfBirth.AddYears(age))
        {
            age--;
        }

        if (age < 0)
        {
            throw new ArgumentException("Date of birth cannot be in the future.", nameof(dateOfBirth));
        }

        return age;
    }

    /// <summary>
    /// Gets the start of the day (00:00:00) for the given DateTime.
    /// </summary>
    /// <param name="date">The DateTime value.</param>
    /// <returns>A DateTime representing the start of the day.</returns>
    public static DateTime StartOfDay(this DateTime date)
    {
        return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, date.Kind);
    }

    /// <summary>
    /// Gets the end of the day (23:59:59.999) for the given DateTime.
    /// </summary>
    /// <param name="date">The DateTime value.</param>
    /// <returns>A DateTime representing the end of the day.</returns>
    public static DateTime EndOfDay(this DateTime date)
    {
        return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999, date.Kind);
    }
}