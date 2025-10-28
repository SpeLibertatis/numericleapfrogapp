#region

using Microsoft.Extensions.Options;
using NumericLeapFrog.Configuration.Options;

#endregion

namespace NumericLeapFrog.Infrastructure.Options.Validation;

/// <summary>
///     Validates <see cref="TypewriterOptions" /> and records non-fatal warnings on invalid settings.
/// </summary>
/// <remarks>
///     Ensures <see cref="TypewriterOptions.DelayMs" /> is non-negative. This validator never throws.
///     When invalid, it writes a message to an <see cref="IOptionsWarningSink" /> and returns
///     <see cref="ValidateOptionsResult.Skip" /> so the application can continue (e.g., with defaults
///     or post-configured fallbacks).
/// </remarks>
internal sealed class TypewriterOptionsValidator(IOptionsWarningSink sink) : IValidateOptions<TypewriterOptions>
{
    /// <summary>
    ///     Validates the provided <paramref name="options" /> instance and records warnings for invalid values.
    /// </summary>
    /// <param name="name">Named options instance (unused).</param>
    /// <param name="options">The bound typewriter options to validate.</param>
    /// <returns>
    ///     <see cref="ValidateOptionsResult.Success" /> when valid; otherwise <see cref="ValidateOptionsResult.Skip" />
    ///     after emitting warnings to the sink. A null instance is treated as valid.
    /// </returns>
    public ValidateOptionsResult Validate(string? name, TypewriterOptions? options)
    {
        if (options is null || options.DelayMs >= 0)
            return ValidateOptionsResult.Success;

        sink.Add("TypewriterOptions: DelayMs must be non-negative.");
        return ValidateOptionsResult.Skip;
    }
}