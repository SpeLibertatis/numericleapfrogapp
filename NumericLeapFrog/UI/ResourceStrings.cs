using NumericLeapFrog.UI.Resources;

namespace NumericLeapFrog.UI;

/// <summary>
/// Provides localized UI strings by adapting values from the strongly-typed resources in <see cref="SR"/>.
/// </summary>
/// <remarks>
/// <see cref="Instructions"/> is composed by concatenating <see cref="SR.Instructions1"/> and
/// <see cref="SR.Instructions2"/> separated by a single space.
/// </remarks>
public sealed class ResourceStrings : IStrings
{
    /// <summary>
    /// Welcome banner shown when the game starts.
    /// </summary>
    public string Welcome => SR.Welcome;

    /// <summary>
    /// Full instructions shown to the player.
    /// </summary>
    /// <remarks>
    /// Constructed from two resource lines for readability in localization files.
    /// </remarks>
    public string Instructions => string.Join(" ", SR.Instructions1, SR.Instructions2);

    /// <summary>
    /// Prompt requesting the player's numeric guess.
    /// </summary>
    public string Prompt => SR.PromptGuess;

    /// <summary>
    /// Validation message for non-numeric input.
    /// </summary>
    public string InvalidNumber => SR.InvalidNumber;

    /// <summary>
    /// Message indicating the player should continue guessing.
    /// </summary>
    public string ContinueMessage => SR.ContinueMessage;

    /// <summary>
    /// Prompt instructing the player to press Enter to continue.
    /// </summary>
    public string ContinuePrompt => SR.ContinuePrompt;

    /// <summary>
    /// Format string for showing the running total.
    /// </summary>
    /// <value>Expects a single argument: the total value (e.g., <c>string.Format(TotalSoFarFormat, total)</c>).</value>
    public string TotalSoFarFormat => SR.TotalSoFar;

    /// <summary>
    /// Message displayed when the player wins.
    /// </summary>
    public string Win => SR.WinMessage;

    /// <summary>
    /// Message displayed when the player loses.
    /// </summary>
    public string Loss => SR.LoseMessage;
}