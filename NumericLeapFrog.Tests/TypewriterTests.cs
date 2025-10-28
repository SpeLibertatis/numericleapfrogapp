using NumericLeapFrog.Configuration.Options;
using NumericLeapFrog.Infrastructure.Abstractions;
using NumericLeapFrog.Infrastructure.Time;
using NumericLeapFrog.UI;

namespace NumericLeapFrog.Tests;

public class TypewriterTests
{
    [Fact]
    public void TypeWrite_WritesCharacters()
    {
        var buf = new BufferConsole();
        var typer = new Typewriter(buf, new TypewriterOptions { DelayMs = 0 }, new NoDelay());
        typer.TypeWrite("Hi");
        Assert.Equal(new[] { "H", "i" }, buf.Writes);
    }

    [Fact]
    public void TypeWriteLine_AppendsNewline()
    {
        var buf = new BufferConsole();
        var typer = new Typewriter(buf, new TypewriterOptions { DelayMs = 0 }, new NoDelay());
        typer.TypeWriteLine("Hi");
        Assert.Equal(["H", "i", "\n"], buf.Writes.Select(w => w == "\n" || w.EndsWith("\n") ? "\n" : w));
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