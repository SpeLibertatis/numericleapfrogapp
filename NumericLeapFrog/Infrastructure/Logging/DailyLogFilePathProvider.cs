using NumericLeapFrog.Configuration.Options;

namespace NumericLeapFrog.Infrastructure.Logging;

/// <summary>
/// Provides a log file path that rolls daily based on configuration.
/// </summary>
/// <remarks>
/// The file name format is <c>{prefix}-YYYYMMDD.log</c> and directory defaults to AppContext.BaseDirectory.
/// </remarks>
public sealed class DailyLogFilePathProvider(LoggingOptions options) : ILogFilePathProvider
{
    /// <summary>
    /// Gets the absolute path to today's log file.
    /// </summary>
    /// <returns>A path combining configured directory and a daily rolling file name.</returns>
    public string GetDailyLogFilePath()
    {
        var dir = options.Directory ?? AppContext.BaseDirectory;
        var now = options.UseUtcForRoll ? DateTime.UtcNow : DateTime.Now;
        var prefix = string.IsNullOrWhiteSpace(options.FileNamePrefix) ? "game" : options.FileNamePrefix;
        return Path.Combine(dir, $"{prefix}-{now:yyyyMMdd}.log");
    }
}