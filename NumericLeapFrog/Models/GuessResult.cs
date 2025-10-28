namespace NumericLeapFrog.Models;

/// <summary>
///     Result of applying a guess to the game state.
/// </summary>
/// <param name="Outcome">The game outcome after the guess.</param>
/// <param name="Total">The running total after the guess.</param>
/// <param name="Target">The target number the player is trying not to exceed.</param>
/// <param name="Difference">Target minus Total (negative indicates over target).</param>
public sealed record GuessResult(GameOutcome Outcome, int Total, int Target, int Difference);