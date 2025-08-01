using Demo.SharedKernel.Core.Abstractions;

namespace Demo.SharedKernel.Core.Services;

/// <summary>
/// Concrete implementation of ITimeProvider that returns the actual system time.
/// This is the default implementation used in production.
/// </summary>
public class SystemClock : ITimeProvider
{
    /// <summary>
    /// Gets the current UTC date and time from the system clock.
    /// </summary>
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}