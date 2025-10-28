#region

using NumericLeapFrog.Configuration.Options;
using NumericLeapFrog.Infrastructure.Abstractions;
using NumericLeapFrog.Infrastructure.Time;
using NumericLeapFrog.UI;

#endregion

namespace NumericLeapFrog.Tests;

public class ConsoleGameUIParsingTests
{
    [Fact]
    public void PromptGuess_ReturnsFalse_On_Invalid()
    {
        var io = new BufferConsole("foo");
        var ui = new ConsoleGameUI(io, new Typewriter(io, new TypewriterOptions { DelayMs = 0 }, new ThreadDelay()),
            new FakeStrings(), new UiOptions());
        var (ok, _) = ui.PromptGuess();
        Assert.False(ok);
    }

    [Fact]
    public void PromptGuess_ReturnsTrue_With_Value()
    {
        var io = new BufferConsole("42");
        var ui = new ConsoleGameUI(io, new Typewriter(io, new TypewriterOptions { DelayMs = 0 }, new ThreadDelay()),
            new FakeStrings(), new UiOptions());
        var (ok, value) = ui.PromptGuess();
        Assert.True(ok);
        Assert.Equal(42, value);
    }

    private sealed class BufferConsole(params string[] inputs) : IConsole
    {
        private readonly Queue<string> _inputs = new(inputs);
        public readonly List<string> Writes = [];

        public void Write(string text)
        {
            Writes.Add(text);
        }

        public void WriteLine(string text)
        {
            Writes.Add(text + "\n");
        }

        public string ReadLine()
        {
            return _inputs.Count > 0 ? _inputs.Dequeue() : string.Empty;
        }

        public void Clear()
        {
        }
    }

    private sealed class FakeStrings : IStrings
    {
        public string Welcome => "";
        public string Instructions => "";
        public string Prompt => "Enter:";
        public string InvalidNumber => "bad";
        public string ContinueMessage => "";
        public string ContinuePrompt => "";
        public string TotalSoFarFormat => "{0}";
        public string Win => "";
        public string Loss => "";
    }
}