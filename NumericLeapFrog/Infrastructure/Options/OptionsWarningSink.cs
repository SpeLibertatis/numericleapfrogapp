using System.Collections.Concurrent;

namespace NumericLeapFrog.Infrastructure.Options;

public sealed class OptionsWarningSink : IOptionsWarningSink
{
 private readonly ConcurrentQueue<string> _warnings = new();

 public void Add(string message)
 {
 if (!string.IsNullOrWhiteSpace(message))
 _warnings.Enqueue(message);
 }

 public IReadOnlyList<string> Snapshot()
 {
 return _warnings.ToArray();
 }

 public IReadOnlyList<string> Drain()
 {
 var list = new List<string>();
 while (_warnings.TryDequeue(out var msg))
 list.Add(msg);
 return list;
 }
}
