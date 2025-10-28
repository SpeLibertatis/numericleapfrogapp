using System.Resources;

namespace NumericLeapFrog.Domain.Resources;

/// <summary>
/// Strongly-typed accessor for domain and logging string resources embedded in this assembly.
/// </summary>
internal static class SR
{
 private static readonly ResourceManager _rm = new("NumericLeapFrog.Domain.Resources.Strings", typeof(SR).Assembly);

 public static string LogTargetGenerated => _rm.GetString("LogTargetGenerated")!;
 public static string LogInvalidInput => _rm.GetString("LogInvalidInput")!;
 public static string LogUserGuessReceivedTemplate => _rm.GetString("LogUserGuessReceivedTemplate")!;
 public static string LogOutcomeDebugTemplate => _rm.GetString("LogOutcomeDebugTemplate")!;
 public static string LogFinishedOutcomeTemplate => _rm.GetString("LogFinishedOutcomeTemplate")!;

 public static string AppStarting => _rm.GetString("AppStarting")!;
 public static string AppExiting => _rm.GetString("AppExiting")!;
}
