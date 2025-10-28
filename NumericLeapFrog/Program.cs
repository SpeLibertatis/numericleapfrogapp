using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NumericLeapFrog.Domain.BusinessLogic;
using NumericLeapFrog.Domain.Models;
using NumericLeapFrog.Infrastructure.Abstractions;
using NumericLeapFrog.Infrastructure.Console;
using NumericLeapFrog.Infrastructure.Logging;
using NumericLeapFrog.Infrastructure.Time;
using NumericLeapFrog.UI;
using DomainSR = NumericLeapFrog.Domain.Resources.SR;

namespace NumericLeapFrog;

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
                    .AddProvider(new FileLoggerProvider(pathProvider.GetDailyLogFilePath()));
            });
            return loggerFactory.CreateLogger("NumericLeapFrog");
        });

        // Infrastructure
        services.AddSingleton<IConsole, SystemConsole>();
        services.AddSingleton<IDelay, ThreadDelay>();
        services.AddSingleton<Typewriter>();
        services.AddSingleton<IStrings, ResourceStrings>();

        // Domain + UI
        services.AddSingleton<IRandomNumberGenerator, RandomNumberGenerator>();
        services.AddSingleton<IGameUI, ConsoleGameUI>();
        services.AddSingleton<IGameRunner, GameRunner>();

        var provider = services.BuildServiceProvider();
        var logger = provider.GetRequiredService<ILogger>();
        logger.LogInformation(DomainSR.AppStarting);

        var runner = provider.GetRequiredService<IGameRunner>();
        runner.Run();

        logger.LogInformation(DomainSR.AppExiting);
        provider.GetRequiredService<IGameUI>().PauseAtEnd();
    }
}