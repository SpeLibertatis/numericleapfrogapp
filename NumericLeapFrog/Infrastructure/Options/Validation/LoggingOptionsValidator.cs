#region

using Microsoft.Extensions.Options;
using NumericLeapFrog.Configuration.Options;

#endregion

namespace NumericLeapFrog.Infrastructure.Options.Validation;

/// <summary>
///     Validates <see cref="LoggingOptions" /> and records non-fatal warnings on invalid settings.
/// </summary>
/// <remarks>
///     Ensures required fields are present: a non-empty <see cref="LoggingOptions.FileNamePrefix" />
///     and a non-empty <see cref="LoggingOptions.TimestampFormat" />. This validator never throws.
///     When invalid, it writes messages to an <see cref="IOptionsWarningSink" /> and returns
///     <see cref="ValidateOptionsResult.Skip" /> so the application can continue (e.g., with defaults
///     or post-configured fallbacks).
/// </remarks>
internal sealed class LoggingOptionsValidator(IOptionsWarningSink sink) : IValidateOptions<LoggingOptions>
{
    /// <summary>
    ///     Validates the provided <paramref name="options" /> instance and records warnings for invalid values.
    /// </summary>
    /// <param name="name">Named options instance (unused).</param>
    /// <param name="options">The bound logging options to validate.</param>
    /// <returns>
    ///     <see cref="ValidateOptionsResult.Success" /> when valid; otherwise <see cref="ValidateOptionsResult.Skip" />
    ///     after emitting warnings to the sink. A null instance is treated as valid.
    /// </returns>
    public ValidateOptionsResult Validate(string? name, LoggingOptions? options)
    {
        if (options is null) return ValidateOptionsResult.Success;
        if (string.IsNullOrWhiteSpace(options.FileNamePrefix))
        {
            sink.Add("LoggingOptions: FileNamePrefix must be provided.");
            return ValidateOptionsResult.Skip;
        }

        if (!string.IsNullOrEmpty(options.TimestampFormat))
            return ValidateOptionsResult.Success;

        sink.Add("LoggingOptions: TimestampFormat must be provided.");
        return ValidateOptionsResult.Skip;
    }
}