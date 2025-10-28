namespace NumericLeapFrog.Infrastructure.Options;

public interface IOptionsWarningSink
{
 void Add(string message);
 IReadOnlyList<string> Snapshot();
 IReadOnlyList<string> Drain();
}
