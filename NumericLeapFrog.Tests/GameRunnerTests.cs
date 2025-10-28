using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using NumericLeapFrog.Domain.BusinessLogic;
using NumericLeapFrog.Domain.Models;
using NumericLeapFrog.UI;

namespace NumericLeapFrog.Tests;

public class GameRunnerTests
{
 private sealed class FakeRng(int fixedValue) : IRandomNumberGenerator
 {
 public int Next(int minInclusive, int maxInclusive) => fixedValue;
 }

 private sealed class RecordingUI : IGameUI
 {
 private readonly Queue<(bool ok, int guess)> _inputs;
 public readonly List<string> Calls = new();
 public RecordingUI(IEnumerable<int> guesses)
 {
 _inputs = new Queue<(bool ok, int guess)>(guesses.Select(g => (true, g)));
 }
 public void ShowGreeting() => Calls.Add("ShowGreeting");
 public void ShowInstructions() => Calls.Add("ShowInstructions");
 public (bool ok, int guess) PromptGuess()
 {
 Calls.Add("PromptGuess");
 return _inputs.Count >0 ? _inputs.Dequeue() : (false,0);
 }
 public void ShowContinue(int total) => Calls.Add($"ShowContinue:{total}");
 public void ShowWin() => Calls.Add("ShowWin");
 public void ShowLoss() => Calls.Add("ShowLoss");
 public void PauseAndClear() => Calls.Add("PauseAndClear");
 public void PauseAtEnd() => Calls.Add("PauseAtEnd");
 }

 private sealed class NoopLogger : ILogger
 {
 public IDisposable BeginScope<TState>(TState state) where TState : notnull => new Noop();
 public bool IsEnabled(LogLevel logLevel) => true;
 public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter) { }
 private sealed class Noop : IDisposable { public void Dispose() { } }
 }

 [Fact]
 public void Run_WinPath_ShowsWin_AndStops()
 {
 var options = new GameOptions { Threshold =3 };
 var rng = new FakeRng(50);
 var ui = new RecordingUI(new[] {47 });
 var logger = new NoopLogger();
 var runner = new GameRunner(ui, rng, options, logger);

 runner.Run();

 Assert.Contains("ShowGreeting", ui.Calls);
 Assert.Contains("ShowInstructions", ui.Calls);
 Assert.Contains("PromptGuess", ui.Calls);
 Assert.Contains("ShowWin", ui.Calls);
 Assert.DoesNotContain(ui.Calls, c => c.StartsWith("ShowContinue"));
 Assert.DoesNotContain(ui.Calls, c => c == "ShowLoss");
 }

 [Fact]
 public void Run_LossPath_ShowsContinue_Then_Loss()
 {
 var options = new GameOptions { Threshold =3 };
 var rng = new FakeRng(30);
 var ui = new RecordingUI(new[] {20,15 });
 var logger = new NoopLogger();
 var runner = new GameRunner(ui, rng, options, logger);

 runner.Run();

 var continueCall = ui.Calls.FirstOrDefault(c => c.StartsWith("ShowContinue"));
 Assert.NotNull(continueCall);
 Assert.Contains("ShowLoss", ui.Calls);
 }
}
