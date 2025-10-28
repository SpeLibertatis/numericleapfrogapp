using NumericLeapFrog.Domain.Models;

namespace NumericLeapFrog.Domain.BusinessLogic;

/// <summary>
///     Core game rules and state for Numeric Leap Frog; independent of I/O.
/// </summary>
/// <param name="target">The target number to reach without exceeding.</param>
/// <param name="threshold">The inclusive distance from target to count as a win.</param>
public sealed class LeapFrogGame(int target, int threshold = 3)
{
    private int Target { get; } = target;
    private int Threshold { get; } = threshold;
    private int Total { get; set; }

    /// <summary>
    ///     Applies a player's guess to the running total and evaluates the outcome.
    /// </summary>
    /// <param name="guess">The number to add to the running total.</param>
    /// <returns>A <see cref="GuessResult" /> describing the new state and outcome.</returns>
    public GuessResult ApplyGuess(int guess)
    {
        Total += guess;
        var diff = Target - Total; // positive => under, negative => over

        if (Math.Abs(diff) <= Threshold)
            return new GuessResult(GameOutcome.Win, Total, Target, diff);

        return Total > Target ?
            new GuessResult(GameOutcome.Lose, Total, Target, diff) :
            new GuessResult(GameOutcome.Continue, Total, Target, diff);
    }
}