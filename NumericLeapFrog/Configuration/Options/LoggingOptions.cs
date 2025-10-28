namespace NumericLeapFrog.Configuration.Options;

/// <summary>
///     Logging configuration options.
/// </summary>
public class LoggingOptions
{
 /// <summary>
 ///     Minimum log level as string (e.g., "Information").
 /// </summary>
 public string MinimumLevel { get; set; } = "Information";

 /// <summary>
 ///     Directory for log files. Null uses AppContext.BaseDirectory.
 /// </summary>
 public string? Directory { get; set; } = null;

 /// <summary>
 ///     File name prefix for log files.
 /// </summary>
 public string FileNamePrefix { get; set; } = "game";

 /// <summary>
 ///     Rolling period (placeholder for future extension).
 /// </summary>
 public string RollingPeriod { get; set; } = "Daily";

 /// <summary>
 ///     Use UTC time when computing roll period.
 /// </summary>
 public bool UseUtcForRoll { get; set; } = true;

 /// <summary>
 ///     Timestamp format used when writing log entries.
 /// </summary>
 public string TimestampFormat { get; set; } = "yyyy-MM-dd HH:mm:ss";

 /// <summary>
 ///     Logger category name.
 /// </summary>
 public string Category { get; set; } = "NumericLeapFrog";

    // Validation helpers
    internal static bool IsValid(LoggingOptions? o)
    {
        return o is not null
               && !string.IsNullOrWhiteSpace(o.FileNamePrefix)
               && !string.IsNullOrEmpty(o.TimestampFormat);
    }

    internal static void Validate(LoggingOptions o)
    {
        if (o is null) throw new ArgumentNullException(nameof(o));
        if (string.IsNullOrWhiteSpace(o.FileNamePrefix))
            throw new ArgumentException("FileNamePrefix must be non-empty.", nameof(o.FileNamePrefix));
        if (string.IsNullOrEmpty(o.TimestampFormat))
            throw new ArgumentException("TimestampFormat must be provided.", nameof(o.TimestampFormat));
    }
}