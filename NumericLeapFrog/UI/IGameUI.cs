namespace NumericLeapFrog.UI;

/// <summary>
///     Defines the user interface contract for the Numeric Leap Frog game.
/// </summary>
/// <remarks>
///     Implementations handle displaying messages, prompting for input, and managing pauses/clears for console or other
///     UIs.
/// </remarks>
public interface IGameUI
{
    /// <summary>
    ///     Displays the initial greeting message to the player.
    /// </summary>
    void ShowGreeting();

    /// <summary>
    ///     Displays the game instructions to the player.
    /// </summary>
    void ShowInstructions();

    /// <summary>
    ///     Prompts the player for a numeric guess and validates the input.
    /// </summary>
    /// <returns>
    ///     A tuple <c>(ok, guess)</c> where <c>ok</c> is true if parsing succeeded.
    ///     When <c>ok</c> is true, <c>guess</c> contains the parsed value; otherwise it is 0.
    /// </returns>
    (bool ok, int guess) PromptGuess();

    /// <summary>
    ///     Shows a continue message and presents the running total.
    /// </summary>
    /// <param name="total">The current cumulative total.</param>
    void ShowContinue(int total);

    /// <summary>
    ///     Displays the win message.
    /// </summary>
    void ShowWin();

    /// <summary>
    ///     Displays the loss message.
    /// </summary>
    void ShowLoss();

    /// <summary>
    ///     Waits for user acknowledgement and then clears the UI surface.
    /// </summary>
    void PauseAndClear();

    /// <summary>
    ///     Waits for user acknowledgement at the end of the game.
    /// </summary>
    void PauseAtEnd();
}