namespace NumericLeapFrog.Infrastructure.Logging;

public interface ILogFilePathProvider
{
    string GetDailyLogFilePath();
}