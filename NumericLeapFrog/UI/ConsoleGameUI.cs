using NumericLeapFrog.Configuration.Options;
using NumericLeapFrog.Infrastructure.Abstractions;

namespace NumericLeapFrog.UI;

/// <summary>
/// Console-based user interface for the Numeric Leap Frog game.
/// </summary>
/// <param name="console">The console abstraction used for input and output.</param>
/// <param name="typer">The typewriter used to render text with a typing effect.</param>
/// <param name="strings">The localized strings provider for all UI messages.</param>
/// <remarks>
/// This implementation relies on <see cref="IConsole"/> and <see cref="Typewriter"/> to enable
/// testability and a more engaging console experience. It implements <see cref="IGameUI"/>.
/// </remarks>
public sealed class ConsoleGameUI(IConsole console, Typewriter typer, IStrings strings, UiOptions uiOptions) : IGameUI
{
 /// <summary>
 /// Displays the initial greeting message to the player.
 /// </summary>
 public void ShowGreeting()
 {
 typer.TypeWriteLine(strings.Welcome);
 }

 /// <summary>
 /// Displays the game instructions and adds a blank line for spacing.
 /// </summary>
 public void ShowInstructions()
 {
 typer.TypeWriteLine(strings.Instructions);
 console.WriteLine(string.Empty);
 }

 /// <summary>
 /// Prompts the player for a numeric guess and validates the input.
 /// </summary>
 /// <returns>
 /// A tuple <c>(ok, guess)</c> where <c>ok</c> is <see langword="true"/> if the input
 /// could be parsed as an <see cref="int"/>, and <see langword="false"/> otherwise.
 /// When <c>ok</c> is <see langword="true"/>, <c>guess</c> contains the parsed value; when
 /// <c>ok</c> is <see langword="false"/>, an invalid-input message is shown and <c>guess</c>
 /// is <c>0</c>.
 /// </returns>
 public (bool ok, int guess) PromptGuess()
 {
 typer.TypeWrite(strings.Prompt);
 var input = console.ReadLine();
 if (int.TryParse(input, out var value)) 
 return (true, value);

 typer.TypeWriteLine(strings.InvalidNumber);
 return (false,0);

 }

 /// <summary>
 /// Shows a continue message, waits for the player to acknowledge, clears the screen,
 /// and then shows the running total.
 /// </summary>
 /// <param name="total">The current cumulative total to display after clearing.</param>
 public void ShowContinue(int total)
 {
 typer.TypeWriteLine(strings.ContinueMessage);
 typer.TypeWriteLine(strings.ContinuePrompt);
 PauseAndClear();
 typer.TypeWriteLine(string.Format(strings.TotalSoFarFormat, total));
 }

 /// <summary>
 /// Displays the win message.
 /// </summary>
 public void ShowWin()
 {
 console.WriteLine(string.Empty);
 typer.TypeWriteLine(strings.Win);
 }

 /// <summary>
 /// Displays the loss message.
 /// </summary>
 public void ShowLoss()
 {
 console.WriteLine(string.Empty);
 typer.TypeWriteLine(strings.Loss);
 }

 /// <summary>
 /// Waits for the user to press Enter and then clears the console.
 /// </summary>
 public void PauseAndClear()
 {
 console.ReadLine();
 console.Clear();
 }

 /// <summary>
 /// Waits for the user to press Enter, typically used before exiting the app.
 /// </summary>
 public void PauseAtEnd()
 {
 if (uiOptions.PauseAtEnd)
 {
 console.ReadLine();
 }
 }
}