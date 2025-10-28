using System;
using System.IO;

namespace NumericLeapFrog.Infrastructure.Logging;

public interface ILogFilePathProvider
{
 string GetDailyLogFilePath();
}

public sealed class DailyLogFilePathProvider : ILogFilePathProvider
{
 public string GetDailyLogFilePath()
 {
 return Path.Combine(AppContext.BaseDirectory, $"game-{DateTime.UtcNow:yyyyMMdd}.log");
 }
}
