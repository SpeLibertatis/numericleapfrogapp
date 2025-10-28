using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using NumericLeapFrog.Domain.BusinessLogic;
using NumericLeapFrog.Domain.Models;
using NumericLeapFrog.Infrastructure.Abstractions;
using NumericLeapFrog.Infrastructure.Console;
using NumericLeapFrog.Infrastructure.Logging;
using NumericLeapFrog.Infrastructure.Time;
using NumericLeapFrog.UI;
using DomainSR = NumericLeapFrog.Domain.Resources.SR;
using GameConfigOptions = NumericLeapFrog.Configuration.Options.GameOptions;
using TypewriterOptions = NumericLeapFrog.Configuration.Options.TypewriterOptions;
using UiOptions = NumericLeapFrog.Configuration.Options.UiOptions;
using LoggingOptions = NumericLeapFrog.Configuration.Options.LoggingOptions;
using Microsoft.Extensions.Options;

namespace NumericLeapFrog;

internal static class Program
{
    public static void Main()
    {
        var services = new ServiceCollection();

        // Build configuration (optional appsettings.json + env vars with prefix)
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables(prefix: "NUMERICLEAPFROG_")
            .Build();

        // Bind options (if sections are absent, class defaults remain in effect)
        services.Configure<GameConfigOptions>(config.GetSection("Game"));
        services.Configure<TypewriterOptions>(config.GetSection("Typewriter"));
        services.Configure<UiOptions>(config.GetSection("UI"));
        services.Configure<LoggingOptions>(config.GetSection("Logging"));

        // Register unwrapped options for constructor injection
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<GameConfigOptions>>().Value);
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<TypewriterOptions>>().Value);
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<UiOptions>>().Value);
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<LoggingOptions>>().Value);

        // Options (existing domain GameOptions object)
        services.AddSingleton<GameOptions>();

        // Logging
        services.AddSingleton<ILogFilePathProvider, DailyLogFilePathProvider>();
        services.AddSingleton<ILogger>(sp =>
        {
            var logOpts = sp.GetRequiredService<LoggingOptions>();
            var pathProvider = sp.GetRequiredService<ILogFilePathProvider>();
            var minLevel = Enum.TryParse<LogLevel>(logOpts.MinimumLevel, ignoreCase: true, out var parsed)
                ? parsed
                : LogLevel.Information;
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .SetMinimumLevel(minLevel)
                    .AddProvider(new FileLoggerProvider(pathProvider.GetDailyLogFilePath(), logOpts));
            });
            return loggerFactory.CreateLogger(logOpts.Category);
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