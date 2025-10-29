namespace NumericLeapFrog.Infrastructure.Options;

/// <summary>
///     Collects non-fatal configuration or options validation warnings for deferred logging.
/// </summary>
/// <remarks>
///     Typical usage:
///     - Validators (e.g., <c>IValidateOptions&lt;T&gt;</c>) call <see cref="Add(string)" /> to record issues.
///     - Application startup drains the sink and logs warnings without failing the process.
///     Implementations should be fast and safe to call from multiple threads.
/// </remarks>
public interface IOptionsWarningSink
{
    /// <summary>
    ///     Records a warning message. Implementations may ignore empty or whitespace-only messages.
    /// </summary>
    /// <param name="message">The warning text to record.</param>
    void Add(string message);

    /// <summary>
    ///     Returns all currently recorded warnings and clears the sink.
    /// </summary>
    /// <returns>The collected warnings at the time of the call (order unspecified).</returns>
    IReadOnlyList<string> Drain();
}