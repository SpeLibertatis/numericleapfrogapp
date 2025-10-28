namespace NumericLeapFrog.Domain.Models;

/// <summary>
/// Result of applying a guess to the game state.
/// </summary>
/// <param name="Outcome">Outcome after the guess.</param>
/// <param name="Total">Running total after the guess.</param>
/// <param name="Attempts">Number of guesses attempted so far (inclusive).</param>
/// <param name="Difference">Target minus Total (negative indicates over target). For diagnostics/logging.</param>
public sealed record GuessResult(GuessOutcome Outcome, int Total, int Attempts, int? Difference);
