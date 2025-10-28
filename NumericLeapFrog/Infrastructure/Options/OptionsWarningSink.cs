using System.Collections.Concurrent;

namespace NumericLeapFrog.Infrastructure.Options;

/// <summary>
/// Thread-safe, in-memory implementation of <see cref="IOptionsWarningSink"/>.
/// </summary>
/// <remarks>
/// Uses a lock-free <see cref="ConcurrentQueue{T}"/> to collect warning messages from
/// options validators and configuration code. Ordering is the enqueue order but is not
/// guaranteed across threads. Designed to be registered as a singleton and drained
/// during startup to emit warnings without failing the process.
/// </remarks>
public sealed class OptionsWarningSink : IOptionsWarningSink
{
    private readonly ConcurrentQueue<string> _warnings = new();

    /// <summary>
    /// Adds a non-empty warning message to the sink.
    /// </summary>
    /// <param name="message">The warning text to record.</param>
    public void Add(string message)
    {
        if (!string.IsNullOrWhiteSpace(message))
            _warnings.Enqueue(message);
    }

    /// <summary>
    /// Returns a snapshot of the currently recorded warnings without clearing them.
    /// </summary>
    /// <returns>An array copy of the queued warnings at the time of the call.</returns>
    public IReadOnlyList<string> Snapshot()
    {
        return _warnings.ToArray();
    }

    /// <summary>
    /// Returns all currently recorded warnings and clears the sink.
    /// </summary>
    /// <returns>A list of warnings drained from the queue.</returns>
    public IReadOnlyList<string> Drain()
    {
        var list = new List<string>();
        while (_warnings.TryDequeue(out var msg))
            list.Add(msg);
        return list;
    }
}