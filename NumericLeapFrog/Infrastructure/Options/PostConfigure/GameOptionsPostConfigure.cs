#region

using Microsoft.Extensions.Options;
using NumericLeapFrog.Configuration.Options;

#endregion

namespace NumericLeapFrog.Infrastructure.Options.PostConfigure;

/// <summary>
///     Applies a post-configuration safety net for <see cref="GameOptions" />.
/// </summary>
/// <remarks>
///     This runs after binding and validation. If any invalid combination is detected
///     (e.g., <see cref="GameOptions.TargetMin" /> &gt; <see cref="GameOptions.TargetMax" /> or a negative
///     <see cref="GameOptions.Threshold" />), the instance is reset to class defaults instead of throwing.
///     Use with a validator (e.g., <c>IValidateOptions&lt;GameOptions&gt;</c>) to record warnings while keeping the app
///     running.
/// </remarks>
public sealed class GameOptionsPostConfigure : IPostConfigureOptions<GameOptions>
{
    /// <summary>
    ///     Ensures the provided <paramref name="options" /> is valid; if not, reverts to defaults.
    /// </summary>
    /// <param name="name">The named options instance (unused).</param>
    /// <param name="options">The bound options instance to inspect and correct.</param>
    public void PostConfigure(string? name, GameOptions? options)
    {
        if (options is null) return;
        if (options.TargetMin <= options.TargetMax && options.Threshold >= 0)
            return;

        // Reset to class defaults (non-fatal fallback)
        var defaults = new GameOptions();
        options.TargetMin = defaults.TargetMin;
        options.TargetMax = defaults.TargetMax;
        options.Threshold = defaults.Threshold;
    }
}