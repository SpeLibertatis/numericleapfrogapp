namespace NumericLeapFrog.Helpers;

/// <summary>
///     Renders text with a typing effect to a provided console.
/// </summary>
/// <param name="console">The console abstraction to write to.</param>
/// <param name="delayMs">Delay in milliseconds between characters.</param>
public sealed class Typewriter(IConsole console, int delayMs = 15)
{
    /// <summary>
    ///     Writes text character-by-character without a trailing newline.
    /// </summary>
    /// <param name="text">The text to render.</param>
    public void TypeWrite(string text)
    {
        foreach (var ch in text)
        {
            console.Write(ch.ToString());
            Thread.Sleep(delayMs);
        }
    }

    /// <summary>
    ///     Writes text character-by-character followed by a newline.
    /// </summary>
    /// <param name="text">The text to render.</param>
    public void TypeWriteLine(string text)
    {
        TypeWrite(text);
        console.WriteLine("");
    }
}