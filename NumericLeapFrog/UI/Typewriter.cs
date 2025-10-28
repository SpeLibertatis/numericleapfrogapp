#region

using NumericLeapFrog.Configuration.Options;
using NumericLeapFrog.Infrastructure.Abstractions;
using NumericLeapFrog.Infrastructure.Time;

#endregion

namespace NumericLeapFrog.UI;

/// <summary>
///     Renders text with a typewriter effect to a provided console.
/// </summary>
/// <param name="console">The console abstraction to write to.</param>
/// <param name="options">Typewriter options (uses <see cref="TypewriterOptions.DelayMs" />).</param>
/// <param name="delay">Delay provider to space characters and enable testability.</param>
/// <remarks>
///     Each character is written with a fixed delay, in milliseconds, defined by
///     <see cref="TypewriterOptions.DelayMs" />. This type does not provide internal
///     synchronization; callers should ensure single-threaded access if required by
///     the underlying <see cref="IConsole" /> implementation.
/// </remarks>
public sealed class Typewriter(IConsole console, TypewriterOptions options, IDelay delay)
{
    /// <summary>
    ///     Writes text character-by-character without a trailing newline.
    /// </summary>
    /// <param name="text">The text to render.</param>
    /// <param name="ct">Optional cancellation token to stop rendering early.</param>
    /// <remarks>
    ///     Inserts a delay of <see cref="TypewriterOptions.DelayMs" /> between each character.
    ///     If <paramref name="ct" /> is signaled, rendering stops and no newline is written.
    /// </remarks>
    public void TypeWrite(string text, CancellationToken ct = default)
    {
        foreach (var ch in text)
        {
            console.Write(ch.ToString());
            delay.Delay(options.DelayMs, ct);
        }
    }

    /// <summary>
    ///     Writes text character-by-character followed by a newline.
    /// </summary>
    /// <param name="text">The text to render.</param>
    /// <param name="ct">Optional cancellation token to stop rendering early.</param>
    /// <remarks>
    ///     Behaves like <see cref="TypeWrite(string, CancellationToken)" />, then emits a newline via
    ///     <see cref="IConsole.WriteLine(string)" />.
    /// </remarks>
    public void TypeWriteLine(string text, CancellationToken ct = default)
    {
        TypeWrite(text, ct);
        console.WriteLine("");
    }
}