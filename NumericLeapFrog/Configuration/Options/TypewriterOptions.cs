namespace NumericLeapFrog.Configuration.Options;

/// <summary>
/// Typewriter rendering options.
/// </summary>
public class TypewriterOptions
{
 /// <summary>
 /// Delay, in milliseconds, between characters when rendering.
 /// </summary>
 public int DelayMs { get; set; } =20;

 // Validation helpers
 internal static bool IsValid(TypewriterOptions o) => o is not null && o.DelayMs >=0;

 internal static void Validate(TypewriterOptions o)
 {
 if (o is null) throw new ArgumentNullException(nameof(o));
 if (o.DelayMs <0)
 throw new ArgumentOutOfRangeException(nameof(o.DelayMs), "DelayMs must be non-negative.");
 }
}
