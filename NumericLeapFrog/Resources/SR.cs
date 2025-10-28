using System.Resources;

namespace NumericLeapFrog.Resources;

/// <summary>
///     Strongly-typed resource accessor.
/// </summary>
internal static class SR
{
    /// <summary>
    ///     Resource manager that reads strings from the embedded resource file
    ///     <c>NumericLeapFrog.Resources.Strings</c>.
    /// </summary>
    private static readonly ResourceManager _rm = new("NumericLeapFrog.Resources.Strings", typeof(SR).Assembly);

    /// <summary>
    ///     Welcome banner text displayed when the game starts.
    /// </summary>
    public static string Welcome => _rm.GetString("Welcome")!;

    /// <summary>
    ///     First line of instructions explaining the goal of the game.
    /// </summary>
    public static string Instructions1 => _rm.GetString("Instructions1")!;

    /// <summary>
    ///     Second line of instructions describing the rules (accumulation and win condition).
    /// </summary>
    public static string Instructions2 => _rm.GetString("Instructions2")!;

    /// <summary>
    ///     Prompt shown to the player to enter a numeric guess.
    /// </summary>
    public static string PromptGuess => _rm.GetString("PromptGuess")!;

    /// <summary>
    ///     Validation message shown when the input cannot be parsed as an integer.
    /// </summary>
    public static string InvalidNumber => _rm.GetString("InvalidNumber")!;

    /// <summary>
    ///     Message displayed when the player wins the game.
    /// </summary>
    public static string WinMessage => _rm.GetString("WinMessage")!;

    /// <summary>
    ///     Message displayed when the player loses the game.
    /// </summary>
    public static string LoseMessage => _rm.GetString("LoseMessage")!;

    /// <summary>
    ///     Message indicating the player should continue guessing.
    /// </summary>
    public static string ContinueMessage => _rm.GetString("ContinueMessage")!;

    /// <summary>
    ///     Prompt instructing the player to press Enter to continue.
    /// </summary>
    public static string ContinuePrompt => _rm.GetString("ContinuePrompt")!;

    /// <summary>
    ///     Format string for showing the running total; expects one argument (the total value).
    /// </summary>
    public static string TotalSoFar => _rm.GetString("TotalSoFar")!;
}