using NumericLeapFrog.Domain.Models;

namespace NumericLeapFrog.Domain.BusinessLogic;

/// <summary>
///     Core game rules and state for Numeric Leap Frog; independent of I/O.
/// </summary>
/// <param name="target">The target number to reach without exceeding.</param>
/// <param name="options">Game options to configure threshold.</param>
public sealed class LeapFrogGame(int target, GameOptions options)
{
    private int Target { get; } = target;
    private int Threshold { get; } = options.Threshold;
    private int Total { get; set; }
    private int Attempts { get; set; }

    /// <summary>
    ///     Applies a player's guess to the running total and evaluates the outcome.
    /// </summary>
    /// <param name="guess">The number to add to the running total.</param>
    /// <returns>A <see cref="GuessResult" /> describing the new state and outcome.</returns>
    public GuessResult ApplyGuess(int guess)
    {
        Attempts++;
        Total += guess;
        var diff = Target - Total; // positive => under, negative => over

        if (Math.Abs(diff) <= Threshold)
            return new GuessResult(GuessOutcome.Win, Total, Attempts, diff);

        return Total > Target
            ? new GuessResult(GuessOutcome.Loss, Total, Attempts, diff)
            : new GuessResult(GuessOutcome.Continue, Total, Attempts, diff);
    }
}