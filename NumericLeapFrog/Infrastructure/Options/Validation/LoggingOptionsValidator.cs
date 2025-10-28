using Microsoft.Extensions.Options;
using NumericLeapFrog.Configuration.Options;

namespace NumericLeapFrog.Infrastructure.Options.Validation;

internal sealed class LoggingOptionsValidator(IOptionsWarningSink sink) : IValidateOptions<LoggingOptions>
{
 public ValidateOptionsResult Validate(string? name, LoggingOptions options)
 {
 if (options is null) return ValidateOptionsResult.Success;
 if (string.IsNullOrWhiteSpace(options.FileNamePrefix))
 {
 sink.Add("LoggingOptions: FileNamePrefix must be provided.");
 return ValidateOptionsResult.Skip;
 }
 if (string.IsNullOrEmpty(options.TimestampFormat))
 {
 sink.Add("LoggingOptions: TimestampFormat must be provided.");
 return ValidateOptionsResult.Skip;
 }
 return ValidateOptionsResult.Success;
 }
}
