using Microsoft.Extensions.Options;
using NumericLeapFrog.Configuration.Options;

namespace NumericLeapFrog.Infrastructure.Options.Validation;

public sealed class GameOptionsValidator(IOptionsWarningSink sink) : IValidateOptions<GameOptions>
{
 public ValidateOptionsResult Validate(string? name, GameOptions options)
 {
 if (options is null) return ValidateOptionsResult.Success;
 var errors = new List<string>();
 if (options.TargetMin > options.TargetMax)
 errors.Add("GameOptions: TargetMin must be less than or equal to TargetMax.");
 if (options.Threshold <0)
 errors.Add("GameOptions: Threshold must be non-negative.");
 if (errors.Count ==0) return ValidateOptionsResult.Success;
 foreach (var e in errors) sink.Add(e);
 return ValidateOptionsResult.Skip;
 }
}
