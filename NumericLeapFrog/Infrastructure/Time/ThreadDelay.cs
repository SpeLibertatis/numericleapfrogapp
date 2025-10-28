namespace NumericLeapFrog.Infrastructure.Time;

/// <summary>
///     Default <see cref="IDelay" /> implementation that blocks the current thread until the delay completes.
/// </summary>
public sealed class ThreadDelay : IDelay
{
    /// <inheritdoc />
    public void Delay(int ms, CancellationToken ct = default)
    {
        Task.Delay(ms, ct).GetAwaiter().GetResult();
    }
}