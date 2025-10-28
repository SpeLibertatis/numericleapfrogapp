using Microsoft.Extensions.Options;
using NumericLeapFrog.Configuration.Options;

namespace NumericLeapFrog.Infrastructure.Options.PostConfigure;

public sealed class GameOptionsPostConfigure : IPostConfigureOptions<GameOptions>
{
 public void PostConfigure(string? name, GameOptions options)
 {
 if (options is null) return;
 if (options.TargetMin > options.TargetMax || options.Threshold <0)
 {
 // Reset to class defaults
 var defaults = new GameOptions();
 options.TargetMin = defaults.TargetMin;
 options.TargetMax = defaults.TargetMax;
 options.Threshold = defaults.Threshold;
 }
 }
}
