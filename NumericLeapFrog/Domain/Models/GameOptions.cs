namespace NumericLeapFrog.Domain.Models;

public sealed class GameOptions
{
 public int TargetMin { get; init; } =1;
 public int TargetMax { get; init; } =100;
 public int Threshold { get; init; } =5;
 public int TypewriterDelayMs { get; init; } =20;
}
