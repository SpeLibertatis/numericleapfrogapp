using Microsoft.Extensions.Logging;
using NumericLeapFrog.Configuration.Options;

namespace NumericLeapFrog.Infrastructure.Logging;

/// <summary>
///     An <see cref="ILoggerProvider" /> that writes log entries to a single file.
/// </summary>
/// <param name="path">The absolute or relative file path where log entries are appended.</param>
/// <param name="options">Logging options used for formatting.</param>
/// <remarks>
///     Writes are synchronized using a private lock to ensure thread-safe file access.
/// </remarks>
internal sealed class FileLoggerProvider(string path, LoggingOptions options) : ILoggerProvider
{
    private readonly object _gate = new();

    /// <summary>
    ///     Creates a new <see cref="ILogger" /> that writes to the configured file using the specified category name.
    /// </summary>
    /// <param name="categoryName">The category name for messages produced by the logger.</param>
    /// <returns>An <see cref="ILogger" /> instance backed by a file sink.</returns>
    public ILogger CreateLogger(string categoryName)
    {
        return new FileLogger(path, _gate, categoryName, options.TimestampFormat);
    }

    /// <summary>
    ///     Releases resources held by the provider. This implementation has no resources to dispose.
    /// </summary>
    public void Dispose()
    {
    }

    /// <summary>
    ///     <see cref="ILogger" /> implementation that appends log entries to a file.
    /// </summary>
    /// <param name="path">The file path to append log entries to.</param>
    /// <param name="gate">The synchronization object to protect concurrent writes.</param>
    /// <param name="category">The category name associated with this logger.</param>
    /// <param name="timestampFormat">Timestamp format string used for message timestamps.</param>
    private sealed class FileLogger(string path, object gate, string category, string timestampFormat) : ILogger
    {
        /// <inheritdoc />
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null; // Scopes are not persisted in this logger.
        }

        /// <summary>
        ///     Determines whether the given <paramref name="logLevel" /> is enabled.
        /// </summary>
        /// <param name="logLevel">The level to check.</param>
        /// <returns>
        ///     <see langword="false" /> only when the level is <see cref="LogLevel.None" />; otherwise
        ///     <see langword="true" />.
        /// </returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        /// <summary>
        ///     Writes a formatted log message (and optional exception details) to the file.
        /// </summary>
        /// <typeparam name="TState">The type of the state object.</typeparam>
        /// <param name="logLevel">Entry severity.</param>
        /// <param name="eventId">The event id.</param>
        /// <param name="state">The log state.</param>
        /// <param name="exception">An optional exception to include with the message.</param>
        /// <param name="formatter">Formats the <paramref name="state" /> and <paramref name="exception" /> into a message string.</param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            var now = DateTime.Now; // wall-clock for logging display
            var line = $"{now.ToString(timestampFormat)} [{logLevel}] {category}: {formatter(state, exception)}";
            if (exception != null) line += $"{Environment.NewLine}{exception}";
            lock (gate)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path)!);
                File.AppendAllText(path, line + Environment.NewLine);
            }
        }
    }
}