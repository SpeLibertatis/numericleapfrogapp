namespace NumericLeapFrog.Configuration.Options;

/// <summary>
/// Strongly-typed options for game configuration.
/// </summary>
public class GameOptions
{
 /// <summary>
 /// Inclusive minimum value for the target number.
 /// </summary>
 public int TargetMin { get; set; } =1;

 /// <summary>
 /// Exclusive maximum value for the target number.
 /// </summary>
 public int TargetMax { get; set; } =100;

 /// <summary>
 /// Threshold within which the player wins.
 /// </summary>
 public int Threshold { get; set; } =5;

 // Validation helpers
 internal static bool IsValid(GameOptions o) => o is not null && o.TargetMin <= o.TargetMax;

 internal static void Validate(GameOptions o)
 {
 if (o is null) throw new ArgumentNullException(nameof(o));
 if (o.TargetMin > o.TargetMax)
 throw new ArgumentException("TargetMin must be less than or equal to TargetMax.", nameof(o));
 }
}
