namespace NumericLeapFrog.Infrastructure.Logging;

/// <summary>
/// Computes the destination path for application log files.
/// </summary>
/// <remarks>
/// Implementations typically generate a file path that "rolls" daily (e.g., <c>prefix-YYYYMMDD.log</c>)
/// in a configured directory. This interface is read-only: callers expect it to be pure, fast,
/// and thread-safe. It must not create files or write to disk; the logger will ensure the
/// directory exists before writing.
/// </remarks>
public interface ILogFilePathProvider
{
 /// <summary>
 /// Gets the full path to today's log file following the configured rolling convention.
 /// </summary>
 /// <returns>An absolute file system path for the current day's log file.</returns>
 string GetDailyLogFilePath();
}