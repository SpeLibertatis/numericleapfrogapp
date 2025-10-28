namespace NumericLeapFrog.Domain.Models;

/// <summary>
/// Game configuration values used by the core domain logic.
/// </summary>
/// <remarks>
/// This legacy options type is consumed by <see cref="Domain.BusinessLogic.GameRunner"/> and
/// <see cref="Domain.BusinessLogic.LeapFrogGame"/>. UI-related delays have moved to
/// <c>Configuration.Options.TypewriterOptions</c>; the <see cref="TypewriterDelayMs"/> property
/// remains for backward compatibility with existing tests and wiring.
/// </remarks>
public sealed class GameOptions
{
 /// <summary>
 /// Inclusive minimum for the target number randomly selected at the start of a game.
 /// Default is <c>1</c>.
 /// </summary>
 public int TargetMin { get; init; } =1;

 /// <summary>
 /// Inclusive maximum for the target number randomly selected at the start of a game.
 /// Default is <c>100</c>.
 /// </summary>
 public int TargetMax { get; init; } =100;

 /// <summary>
 /// Allowed absolute difference from the target to be considered a win.
 /// For example, with a threshold of <c>5</c>, being within ±5 of the target wins the game.
 /// Default is <c>5</c>.
 /// </summary>
 public int Threshold { get; init; } =5;

 /// <summary>
 /// UI typewriter delay in milliseconds. Retained for compatibility; new code should use
 /// <c>Configuration.Options.TypewriterOptions.DelayMs</c>. Default is <c>20</c>.
 /// </summary>
 public int TypewriterDelayMs { get; init; } =20;
}