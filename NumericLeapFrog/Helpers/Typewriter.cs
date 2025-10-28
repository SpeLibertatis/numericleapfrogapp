using NumericLeapFrog.Domain.Models;
using NumericLeapFrog.Infrastructure.Abstractions;
using NumericLeapFrog.Infrastructure.Time;
using System.Threading;

namespace NumericLeapFrog.UI;

/// <summary>
///     Renders text with a typing effect to a provided console.
/// </summary>
/// <param name="console">The console abstraction to write to.</param>
/// <param name="options">Game options (uses TypewriterDelayMs)</param>
/// <param name="delay">Delay provider for testability</param>
public sealed class Typewriter(IConsole console, GameOptions options, IDelay delay)
{
    /// <summary>
    ///     Writes text character-by-character without a trailing newline.
    /// </summary>
    /// <param name="text">The text to render.</param>
    /// <param name="ct">Optional cancellation token.</param>
    public void TypeWrite(string text, CancellationToken ct = default)
    {
        foreach (var ch in text)
        {
            console.Write(ch.ToString());
            delay.Delay(options.TypewriterDelayMs, ct);
        }
    }

    /// <summary>
    ///     Writes text character-by-character followed by a newline.
    /// </summary>
    /// <param name="text">The text to render.</param>
    /// <param name="ct">Optional cancellation token.</param>
    public void TypeWriteLine(string text, CancellationToken ct = default)
    {
        TypeWrite(text, ct);
        console.WriteLine("");
    }
}