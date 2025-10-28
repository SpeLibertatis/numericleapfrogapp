using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NumericLeapFrog.Domain.BusinessLogic;
using NumericLeapFrog.Configuration.Options;
using NumericLeapFrog.UI;

namespace NumericLeapFrog.Tests;

public class GameRunnerTests
{
    [Fact]
    public void Run_WinPath_ShowsWin_AndStops()
    {
        var options = Options.Create(new GameOptions { Threshold = 3 }).Value;
        var rng = new FakeRng(50);
        var ui = new RecordingUI([47]);
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
        var options = Options.Create(new GameOptions { Threshold = 3 }).Value;
        var rng = new FakeRng(30);
        var ui = new RecordingUI([20, 15]);
        var logger = new NoopLogger();
        var runner = new GameRunner(ui, rng, options, logger);

        runner.Run();

        var continueCall = ui.Calls.FirstOrDefault(c => c.StartsWith("ShowContinue"));
        Assert.NotNull(continueCall);
        Assert.Contains("ShowLoss", ui.Calls);
    }

    private sealed class FakeRng(int fixedValue) : IRandomNumberGenerator
    {
        public int Next(int minInclusive, int maxInclusive)
        {
            return fixedValue;
        }
    }

    private sealed class RecordingUI : IGameUI
    {
        private readonly Queue<(bool ok, int guess)> _inputs;
        public readonly List<string> Calls = [];

        public RecordingUI(IEnumerable<int> guesses)
        {
            _inputs = new Queue<(bool ok, int guess)>(guesses.Select(g => (true, g)));
        }

        public void ShowGreeting()
        {
            Calls.Add("ShowGreeting");
        }

        public void ShowInstructions()
        {
            Calls.Add("ShowInstructions");
        }

        public (bool ok, int guess) PromptGuess()
        {
            Calls.Add("PromptGuess");
            return _inputs.Count > 0 ? _inputs.Dequeue() : (false, 0);
        }

        public void ShowContinue(int total)
        {
            Calls.Add($"ShowContinue:{total}");
        }

        public void ShowWin()
        {
            Calls.Add("ShowWin");
        }

        public void ShowLoss()
        {
            Calls.Add("ShowLoss");
        }

        public void PauseAndClear()
        {
            Calls.Add("PauseAndClear");
        }

        public void PauseAtEnd()
        {
            Calls.Add("PauseAtEnd");
        }
    }

    private sealed class NoopLogger : ILogger
    {
        public IDisposable BeginScope<TState>(TState state) where TState : notnull
        {
            return new Noop();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
        }

        private sealed class Noop : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }
}