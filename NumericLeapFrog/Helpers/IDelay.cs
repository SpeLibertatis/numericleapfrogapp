using System.Threading;
using System.Threading.Tasks;

namespace NumericLeapFrog.Infrastructure.Time;

public interface IDelay
{
 void Delay(int ms, CancellationToken ct = default);
}

public sealed class ThreadDelay : IDelay
{
 public void Delay(int ms, CancellationToken ct = default)
 {
 Task.Delay(ms, ct).GetAwaiter().GetResult();
 }
}
