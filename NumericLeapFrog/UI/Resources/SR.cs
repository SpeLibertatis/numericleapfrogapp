#region

using System.Resources;

#endregion

namespace NumericLeapFrog.UI.Resources;

/// <summary>
///     Strongly-typed accessor for UI string resources embedded in this assembly.
/// </summary>
/// <remarks>
///     Values are retrieved from the <c>NumericLeapFrog.UI.Resources.Strings</c> resource set
///     using a shared <see cref="ResourceManager" /> instance. Lookups are culture-aware and
///     thread-safe for concurrent reads.
/// </remarks>
/// <seealso cref="ResourceManager" />
internal static class SR
{
    /// <summary>
    ///     Resource manager used to resolve localized strings from
    ///     <c>NumericLeapFrog.UI.Resources.Strings</c> in this assembly.
    /// </summary>
    private static readonly ResourceManager _rm = new("NumericLeapFrog.UI.Resources.Strings", typeof(SR).Assembly);

    /// <summary>
    ///     Welcome banner text displayed when the game starts.
    /// </summary>
    /// <value>The localized string for resource key <c>Welcome</c>.</value>
    public static string Welcome => _rm.GetString("Welcome")!;

    /// <summary>
    ///     First line of instructions explaining the goal of the game.
    /// </summary>
    /// <value>The localized string for resource key <c>Instructions1</c>.</value>
    public static string Instructions1 => _rm.GetString("Instructions1")!;

    /// <summary>
    ///     Second line of instructions describing the rules (accumulation and win condition).
    /// </summary>
    /// <value>The localized string for resource key <c>Instructions2</c>.</value>
    public static string Instructions2 => _rm.GetString("Instructions2")!;

    /// <summary>
    ///     Prompt shown to the player to enter a numeric guess.
    /// </summary>
    /// <value>The localized string for resource key <c>PromptGuess</c>.</value>
    public static string PromptGuess => _rm.GetString("PromptGuess")!;

    /// <summary>
    ///     Validation message shown when the input cannot be parsed as an integer.
    /// </summary>
    /// <value>The localized string for resource key <c>InvalidNumber</c>.</value>
    public static string InvalidNumber => _rm.GetString("InvalidNumber")!;

    /// <summary>
    ///     Message displayed when the player wins the game.
    /// </summary>
    /// <value>The localized string for resource key <c>WinMessage</c>.</value>
    public static string WinMessage => _rm.GetString("WinMessage")!;

    /// <summary>
    ///     Message displayed when the player loses the game.
    /// </summary>
    /// <value>The localized string for resource key <c>LoseMessage</c>.</value>
    public static string LoseMessage => _rm.GetString("LoseMessage")!;

    /// <summary>
    ///     Message indicating the player should continue guessing.
    /// </summary>
    /// <value>The localized string for resource key <c>ContinueMessage</c>.</value>
    public static string ContinueMessage => _rm.GetString("ContinueMessage")!;

    /// <summary>
    ///     Prompt instructing the player to press Enter to continue.
    /// </summary>
    /// <value>The localized string for resource key <c>ContinuePrompt</c>.</value>
    public static string ContinuePrompt => _rm.GetString("ContinuePrompt")!;

    /// <summary>
    ///     Format string for showing the running total.
    /// </summary>
    /// <value>The localized format string for resource key <c>TotalSoFar</c>.</value>
    /// <remarks>
    ///     Expects a single argument: the current total value (e.g., <c>string.Format(SR.TotalSoFar, total)</c>).
    /// </remarks>
    public static string TotalSoFar => _rm.GetString("TotalSoFar")!;
}