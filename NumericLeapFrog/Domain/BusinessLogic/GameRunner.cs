using Microsoft.Extensions.Logging;
using NumericLeapFrog.Domain.Models;
using NumericLeapFrog.UI;
using static NumericLeapFrog.Domain.Resources.SR;

namespace NumericLeapFrog.Domain.BusinessLogic;

/// <summary>
/// Orchestrates the Numeric Leap Frog game flow.
/// </summary>
/// <param name="ui">The user interface used to display messages and read input.</param>
/// <param name="rng">Random number generator used to select the game target.</param>
/// <param name="options">Game configuration options (target range, thresholds, etc.).</param>
/// <param name="logger">Logger for game lifecycle and diagnostic events.</param>
public sealed class GameRunner(IGameUI ui, IRandomNumberGenerator rng, GameOptions options, ILogger logger)
    : IGameRunner
{
    /// <summary>
    /// Executes the game by generating a target, greeting the player, showing instructions,
    /// and running the main game loop until completion.
    /// </summary>
    public void Run()
    {
        var target = rng.Next(options.TargetMin, options.TargetMax);
        var game = new LeapFrogGame(target, options);
        logger.LogInformation(LogTargetGenerated);

        ui.ShowGreeting();
        ui.ShowInstructions();
        RunGameLoop(game);
    }

    /// <summary>
    /// Runs the primary loop that processes user guesses and updates game state until
    /// the player wins or loses.
    /// </summary>
    /// <param name="game">The game instance maintaining state and rules.</param>
    private void RunGameLoop(LeapFrogGame game)
    {
        while (true)
        {
            var (ok, guess) = ui.PromptGuess();
            if (!ok)
            {
                logger.LogWarning(LogInvalidInput);
                continue;
            }

            logger.LogInformation(LogUserGuessReceivedTemplate, guess);
            var result = game.ApplyGuess(guess);
            logger.LogDebug(LogOutcomeDebugTemplate, result.Outcome,
                result.Total, result.Difference, result.Attempts);

            switch (result.Outcome)
            {
                case GuessOutcome.Win:
                    ui.ShowWin();
                    logger.LogInformation(LogFinishedOutcomeTemplate, result.Outcome);
                    return;
                case GuessOutcome.Loss:
                    ui.ShowLoss();
                    logger.LogInformation(LogFinishedOutcomeTemplate, result.Outcome);
                    return;
                case GuessOutcome.Continue:
                default:
                    ui.ShowContinue(result.Total);
                    break;
            }
        }
    }
}