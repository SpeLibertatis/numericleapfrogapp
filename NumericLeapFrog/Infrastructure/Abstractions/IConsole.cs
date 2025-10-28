namespace NumericLeapFrog.Infrastructure.Abstractions;

/// <summary>
/// Abstraction for console operations used by the application.
/// </summary>
public interface IConsole
{
 /// <summary>
 /// Writes text to the output without a newline.
 /// </summary>
 /// <param name="text">The text to write.</param>
 void Write(string text);

 /// <summary>
 /// Writes text to the output followed by a newline.
 /// </summary>
 /// <param name="text">The text to write.</param>
 void WriteLine(string text);

 /// <summary>
 /// Reads a line of input from the user.
 /// </summary>
 /// <returns>The input line, or null if no input was available.</returns>
 string? ReadLine();

 /// <summary>
 /// Clears the console display.
 /// </summary>
 void Clear();
}
