using NumericLeapFrog.Domain.Models;
using NumericLeapFrog.Infrastructure.Abstractions;
using NumericLeapFrog.Infrastructure.Logging;
using NumericLeapFrog.Infrastructure.Time;
using NumericLeapFrog.UI;
using NumericLeapFrog.Domain.BusinessLogic;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.IO;

namespace NumericLeapFrog.Composition;

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

        var options = new GameOptions();

        IConsole io = new Infrastructure.Console.SystemConsole();
        IDelay delay = new ThreadDelay();
        var typer = new Typewriter(io, options, delay);

        IStrings strings = new ResourceStrings();
        IGameUI ui = new ConsoleGameUI(io, typer, strings);

        IRandomNumberGenerator rng = new RandomNumberGenerator();
        var runner = new GameRunner(ui, rng, options, logger);

        runner.Run();

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
}