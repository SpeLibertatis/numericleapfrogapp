#region

using Microsoft.Extensions.Options;
using NumericLeapFrog.Configuration.Options;

#endregion

namespace NumericLeapFrog.Infrastructure.Options.Validation;

/// <summary>
///     Validates <see cref="GameOptions" /> values and records non-fatal warnings when invalid.
/// </summary>
/// <remarks>
///     This validator checks that <see cref="GameOptions.TargetMin" /> is less than or equal to
///     <see cref="GameOptions.TargetMax" /> and that <see cref="GameOptions.Threshold" /> is non-negative.
///     It never throws. On validation failures it writes messages to an <see cref="IOptionsWarningSink" />
///     and returns <see cref="ValidateOptionsResult.Skip" /> so the application can continue using defaults
///     or post-configured fallbacks.
/// </remarks>
public sealed class GameOptionsValidator(IOptionsWarningSink sink) : IValidateOptions<GameOptions>
{
    /// <summary>
    ///     Validates the provided <paramref name="options" /> and records warnings for invalid settings.
    /// </summary>
    /// <param name="name">Named options instance (unused).</param>
    /// <param name="options">The options instance to validate.</param>
    /// <returns>
    ///     <see cref="ValidateOptionsResult.Success" /> when valid or <see cref="ValidateOptionsResult.Skip" /> when
    ///     invalid (after emitting warnings to the sink). A null instance is treated as valid.
    /// </returns>
    public ValidateOptionsResult Validate(string? name, GameOptions? options)
    {
        if (options is null)
            return ValidateOptionsResult.Success;
        var errors = new List<string>();
        if (options.TargetMin > options.TargetMax)
            errors.Add("GameOptions: TargetMin must be less than or equal to TargetMax.");
        if (options.Threshold < 0)
            errors.Add("GameOptions: Threshold must be non-negative.");
        if (errors.Count == 0)
            return ValidateOptionsResult.Success;
        foreach (var e in errors)
            sink.Add(e);
        return ValidateOptionsResult.Skip;
    }
}