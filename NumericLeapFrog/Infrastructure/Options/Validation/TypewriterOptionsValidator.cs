using Microsoft.Extensions.Options;
using NumericLeapFrog.Configuration.Options;

namespace NumericLeapFrog.Infrastructure.Options.Validation;

internal sealed class TypewriterOptionsValidator(IOptionsWarningSink sink) : IValidateOptions<TypewriterOptions>
{
 public ValidateOptionsResult Validate(string? name, TypewriterOptions options)
 {
 if (options is null) return ValidateOptionsResult.Success;
 if (options.DelayMs <0)
 {
 sink.Add("TypewriterOptions: DelayMs must be non-negative.");
 return ValidateOptionsResult.Skip;
 }
 return ValidateOptionsResult.Success;
 }
}
