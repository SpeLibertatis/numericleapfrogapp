using Microsoft.Extensions.Options;
using NumericLeapFrog.Configuration.Options;
using NumericLeapFrog.Infrastructure.Options;
using NumericLeapFrog.Infrastructure.Options.Validation;

namespace NumericLeapFrog.Tests;

public class OptionsValidationTests
{
 [Fact]
 public void GameOptions_Invalid_MinGreaterThanMax_ProducesWarningAndSkip()
 {
 var sink = new OptionsWarningSink();
 var validator = new GameOptionsValidator(sink);
 var opts = new GameOptions { TargetMin =10, TargetMax =1, Threshold =5 };
 var result = validator.Validate(string.Empty, opts);
 Assert.Equal(ValidateOptionsResult.Skip, result);
 var messages = sink.Snapshot();
 Assert.Contains(messages, m => m.Contains("TargetMin") && m.Contains("TargetMax"));
 }
}
