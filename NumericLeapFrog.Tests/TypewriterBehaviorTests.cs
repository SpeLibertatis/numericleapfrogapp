using NumericLeapFrog.Domain.Models;
using NumericLeapFrog.Infrastructure.Abstractions;
using NumericLeapFrog.Infrastructure.Time;
using NumericLeapFrog.UI;

namespace NumericLeapFrog.Tests;

public class TypewriterBehaviorTests
{
    [Fact]
    public void TypeWrite_Writes_InOrder_WithoutDelay()
    {
        var buf = new BufferConsole();
        var tw = new Typewriter(buf, new GameOptions { TypewriterDelayMs = 0 }, new NoDelay());
        tw.TypeWrite("ABC");
        Assert.Equal(["A", "B", "C"], buf.Writes);
    }

    [Fact]
    public void TypeWriteLine_Appends_Newline()
    {
        var buf = new BufferConsole();
        var tw = new Typewriter(buf, new GameOptions { TypewriterDelayMs = 0 }, new NoDelay());
        tw.TypeWriteLine("OK");
        Assert.Equal(["O", "K", "\n"], buf.Writes.Select(w => w == "\n" || w.EndsWith("\n") ? "\n" : w));
    }

    private sealed class BufferConsole : IConsole
    {
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
            return string.Empty;
        }

        public void Clear()
        {
        }
    }

    private sealed class NoDelay : IDelay
    {
        public void Delay(int ms, CancellationToken ct = default)
        {
        }
    }
}