using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NumericLeapFrog.Domain.BusinessLogic;
using NumericLeapFrog.Domain.Models;
using NumericLeapFrog.Infrastructure.Abstractions;
using NumericLeapFrog.Infrastructure.Logging;
using NumericLeapFrog.Infrastructure.Time;
using NumericLeapFrog.UI;

namespace NumericLeapFrog.Composition;

internal static class Program
{
    public static void Main()
    {
        var services = new ServiceCollection();

        // Options
        services.AddSingleton<GameOptions>();

        // Logging
        services.AddSingleton<ILogFilePathProvider, DailyLogFilePathProvider>();
        services.AddSingleton<ILogger>(sp =>
        {
            var pathProvider = sp.GetRequiredService<ILogFilePathProvider>();
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .SetMinimumLevel(LogLevel.Information)
                    .AddSimpleConsole(options =>
                    {
                        options.TimestampFormat = "yyyy-MM-dd HH:mm:ss ";
                        options.SingleLine = true;
                        options.IncludeScopes = false;
                    })
                    .AddProvider(new FileLoggerProvider(pathProvider.GetDailyLogFilePath()));
            });
            return loggerFactory.CreateLogger("NumericLeapFrog");
        });

        // Infrastructure
        services.AddSingleton<IConsole, Infrastructure.Console.SystemConsole>();
        services.AddSingleton<IDelay, ThreadDelay>();
        services.AddSingleton<Typewriter>();
        services.AddSingleton<IStrings, ResourceStrings>();

        // Domain + UI
        services.AddSingleton<IRandomNumberGenerator, RandomNumberGenerator>();
        services.AddSingleton<IGameUI, ConsoleGameUI>();
        services.AddSingleton<IGameRunner, GameRunner>();

        var provider = services.BuildServiceProvider();
        var logger = provider.GetRequiredService<ILogger>();
        logger.LogInformation("Application starting");

        var runner = provider.GetRequiredService<IGameRunner>();
        runner.Run();

        logger.LogInformation("Application exiting");
        provider.GetRequiredService<IGameUI>().PauseAtEnd();
    }
}