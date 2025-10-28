namespace NumericLeapFrog.Infrastructure.Logging;

/// <summary>
/// Provides a log file path that rolls daily based on the current UTC date.
/// </summary>
/// <remarks>
/// The file name format is <c>game-YYYYMMDD.log</c> and it is placed in <see cref="AppContext.BaseDirectory"/>.
/// Using UTC avoids day-boundary issues across time zones.
/// </remarks>
public sealed class DailyLogFilePathProvider : ILogFilePathProvider
{
    /// <summary>
    /// Gets the absolute path to today's log file.
    /// </summary>
    /// <returns>
    /// A path combining <see cref="AppContext.BaseDirectory"/> and a daily rolling file name
    /// (e.g., <c>game-20250131.log</c>).
    /// </returns>
    public string GetDailyLogFilePath()
    {
        return Path.Combine(AppContext.BaseDirectory, $"game-{DateTime.UtcNow:yyyyMMdd}.log");
    }
}