namespace NumericLeapFrog.Models;

/// <summary>
///     Possible outcomes after applying a guess.
/// </summary>
public enum GameOutcome
{
    /// <summary>
    ///     The game should continue with further guesses.
    /// </summary>
    Continue = 0,

    /// <summary>
    ///     The player won (total is within threshold of the target).
    /// </summary>
    Win = 1,

    /// <summary>
    ///     The player lost (total exceeded the target).
    /// </summary>
    Lose = 2
}