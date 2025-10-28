using Microsoft.Extensions.Logging;
using NumericLeapFrog.BusinessLogic;
using NumericLeapFrog.Helpers;
using NumericLeapFrog.Models;
using NumericLeapFrog.Resources;
using System.Globalization;
using System.IO;

namespace NumericLeapFrog;

/// <summary>
///     Application entry point and orchestration for the Numeric Leap Frog game.
///     Coordinates I/O (via <see cref="IConsole" /> and <see cref="Typewriter" />), resources, and core game rules.
/// </summary>
internal static class Program
{
    /// <summary>
    ///     Program entry point. Composes dependencies and starts the game loop.
    /// </summary>
    /// <remarks>
    ///     This method performs simple composition without a DI container to keep the sample minimal.
    /// </remarks>
    public static void Main()
    {
        // Configure standardized local logging under %LOCALAPPDATA%/NumericLeapFrog/logs/YYYY-MM-DD.log
        using var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder
                .SetMinimumLevel(LogLevel.Information)
                .AddSimpleConsole(options =>
                {
                    options.TimestampFormat = "yyyy-MM-dd HH:mm:ss ";
                    options.SingleLine = true;
                    options.IncludeScopes = false;
                })
                .AddProvider(new FileLoggerProvider(GetDailyLogFilePath()));
        });
        var logger = loggerFactory.CreateLogger("NumericLeapFrog");

        logger.LogInformation("Application starting");

        IConsole io = new SystemConsole();
        var typer = new Typewriter(io);
        IRandomNumberGenerator rng = new RandomNumberGenerator();

        // Generate a random target in the inclusive range [1,999]
        var target = rng.NextInclusive(1, 999);
        var game = new LeapFrogGame(target);
        logger.LogInformation("Target generated: {Target}", target);

        Greeting(typer);
        Instructions(typer, io);

        RunGameLoop(io, typer, game, logger);

        logger.LogInformation("Application exiting");
        // Final pause so the console does not close immediately after game ends
        io.ReadLine();
    }

    private static string GetDailyLogFilePath()
    {
        var baseDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var appDir = Path.Combine(baseDir, "NumericLeapFrog", "logs");
        Directory.CreateDirectory(appDir);
        var fileName = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + ".log";
        return Path.Combine(appDir, fileName);
    }

    /// <summary>
    ///     Displays the game greeting using the typewriter effect.
    /// </summary>
    /// <param name="typer">Typewriter used to render text.</param>
    private static void Greeting(Typewriter typer)
    {
        typer.TypeWriteLine(SR.Welcome);
    }

    /// <summary>
    ///     Displays the game instructions loaded from resources.
    /// </summary>
    /// <param name="typer">Typewriter used to render text.</param>
    /// <param name="io">Console used for layout (blank line).</param>
    private static void Instructions(Typewriter typer, IConsole io)
    {
        typer.TypeWriteLine(SR.Instructions1);
        typer.TypeWriteLine(SR.Instructions2);
        io.WriteLine(string.Empty);
    }

    /// <summary>
    ///     Runs the main game loop until the player wins or loses.
    /// </summary>
    /// <param name="io">Console abstraction for input/output.</param>
    /// <param name="typer">Typewriter for display effects.</param>
    /// <param name="game">Game rules and state to apply guesses to.</param>
    /// <param name="logger">Logger for diagnostic events.</param>
    /// <remarks>
    ///     The loop continues prompting for input, applying guesses to game state, and reacting
    ///     to outcomes. It terminates when a win or loss is detected by <see cref="HandleOutcome" />.
    /// </remarks>
    private static void RunGameLoop(IConsole io, Typewriter typer, LeapFrogGame game, ILogger logger)
    {
        while (true)
        {
            // Prompt for the next guess and validate numeric input
            typer.TypeWrite(SR.PromptGuess);
            if (!TryReadInt(io, typer, out var guess))
            {
                logger.LogWarning("Invalid input provided by user");
                // Invalid input: show an error and re-prompt
                continue;
            }

            logger.LogInformation("User guess received: {Guess}", guess);
            var result = game.ApplyGuess(guess);
            logger.LogDebug("Outcome: {Outcome}, Total: {Total}, Target: {Target}", result.Outcome, result.Total, result.Target);

            // Stop looping when the outcome indicates the game is over
            if (HandleOutcome(io, typer, result))
            {
                logger.LogInformation("Game finished with outcome {Outcome}", result.Outcome);
                break;
            }
        }
    }

    /// <summary>
    ///     Attempts to read an integer from the console.
    /// </summary>
    /// <param name="io">Console abstraction used for input.</param>
    /// <param name="typer">Typewriter used to display validation messages.</param>
    /// <param name="value">The parsed integer value, when successful.</param>
    /// <returns>True if a valid integer was read; otherwise, false.</returns>
    private static bool TryReadInt(IConsole io, Typewriter typer, out int value)
    {
        var input = io.ReadLine();
        if (!int.TryParse(input, out value))
        {
            // Notify the user and let the caller decide to re-prompt
            typer.TypeWriteLine(SR.InvalidNumber);
            return false;
        }

        return true;
    }

    /// <summary>
    ///     Handles the display and flow for a single guess outcome.
    /// </summary>
    /// <param name="io">Console abstraction for output.</param>
    /// <param name="typer">Typewriter for display effects.</param>
    /// <param name="result">The result returned from applying the user's guess.</param>
    /// <returns>
    ///     True if the game should end (win or loss); otherwise, false to continue prompting.
    /// </returns>
    private static bool HandleOutcome(IConsole io, Typewriter typer, GuessResult result)
    {
        switch (result.Outcome)
        {
            case GameOutcome.Win:
                HandleWin(io, typer);
                return true;

            case GameOutcome.Lose:
                HandleLoss(io, typer);
                return true;

            case GameOutcome.Continue:
            default:
                HandleContinue(io, typer, result);
                return false;
        }
    }

    /// <summary>
    ///     Displays the continue messages and shows the running total.
    /// </summary>
    /// <param name="io">Console abstraction for input/output.</param>
    /// <param name="typer">Typewriter for display effects.</param>
    /// <param name="result">The result returned from applying the user's guess.</param>
    private static void HandleContinue(IConsole io, Typewriter typer, GuessResult result)
    {
        typer.TypeWriteLine(SR.ContinueMessage);
        typer.TypeWriteLine(SR.ContinuePrompt);
        PauseAndClear(io);
        typer.TypeWriteLine(string.Format(SR.TotalSoFar, result.Total));
    }

    /// <summary>
    ///     Displays the win message.
    /// </summary>
    private static void HandleWin(IConsole io, Typewriter typer)
    {
        io.WriteLine(string.Empty);
        typer.TypeWriteLine(SR.WinMessage);
    }

    /// <summary>
    ///     Displays the loss message.
    /// </summary>
    private static void HandleLoss(IConsole io, Typewriter typer)
    {
        io.WriteLine(string.Empty);
        typer.TypeWriteLine(SR.LoseMessage);
    }

    /// <summary>
    ///     Pauses for user input and then clears the console.
    ///     Used to let the player read messages before refreshing the screen.
    /// </summary>
    private static void PauseAndClear(IConsole io)
    {
        io.ReadLine();
        io.Clear();
    }
}

// Minimal file logger provider using Microsoft.Extensions.Logging abstractions