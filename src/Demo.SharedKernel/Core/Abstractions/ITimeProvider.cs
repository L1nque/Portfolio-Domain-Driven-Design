namespace Demo.SharedKernel.Core.Abstractions;

/// <summary>
/// Defines an abstraction for accessing the current date and time.
/// This allows for easier testing by enabling time to be mocked or controlled.
/// </summary>
public interface ITimeProvider
{
    /// <summary>
    /// Gets the current UTC date and time offset.
    /// </summary>
    DateTimeOffset UtcNow { get; }
}