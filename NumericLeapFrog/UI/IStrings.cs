namespace NumericLeapFrog.UI;

/// <summary>
/// Provides localized UI strings used by the game interface.
/// </summary>
/// <remarks>
/// Implementations typically adapt resource-backed strings. The
/// <see cref="TotalSoFarFormat"/> is a composite format string expecting a
/// single numeric argument (the running total).
/// </remarks>
public interface IStrings
{
 /// <summary>
 /// Welcome banner shown when the game starts.
 /// </summary>
 string Welcome { get; }

 /// <summary>
 /// Full instructions shown to the player.
 /// </summary>
 string Instructions { get; }

 /// <summary>
 /// Prompt requesting the player's numeric guess.
 /// </summary>
 string Prompt { get; }

 /// <summary>
 /// Validation message for non-numeric input.
 /// </summary>
 string InvalidNumber { get; }

 /// <summary>
 /// Message indicating the player should continue guessing.
 /// </summary>
 string ContinueMessage { get; }

 /// <summary>
 /// Prompt instructing the player to press Enter to continue.
 /// </summary>
 string ContinuePrompt { get; }

 /// <summary>
 /// Format string used when showing the running total.
 /// </summary>
 /// <value>
 /// Expects one argument: the total value
 /// (e.g., <c>string.Format(TotalSoFarFormat, total)</c>).
 /// </value>
 string TotalSoFarFormat { get; }

 /// <summary>
 /// Message displayed when the player wins.
 /// </summary>
 string Win { get; }

 /// <summary>
 /// Message displayed when the player loses.
 /// </summary>
 string Loss { get; }
}