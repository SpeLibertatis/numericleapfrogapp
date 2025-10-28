namespace NumericLeapFrog.Infrastructure.Time;

/// <summary>
///     Abstraction for delaying execution.
/// </summary>
/// <remarks>
///     Implementations may block the calling thread or schedule asynchronous delays. This interface
///     is primarily used to make time-dependent code testable.
/// </remarks>
public interface IDelay
{
    /// <summary>
    ///     Delays execution by the specified number of milliseconds.
    /// </summary>
    /// <param name="ms">The delay duration in milliseconds.</param>
    /// <param name="ct">An optional token to observe for cancellation.</param>
    void Delay(int ms, CancellationToken ct = default);
}