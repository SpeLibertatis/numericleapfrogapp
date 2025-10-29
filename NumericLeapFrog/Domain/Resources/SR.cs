#region

using System.Resources;

#endregion

namespace NumericLeapFrog.Domain.Resources;

/// <summary>
///     Strongly-typed accessor for domain and logging string resources embedded in this assembly.
/// </summary>
/// <remarks>
///     Values are resolved from the resource set "NumericLeapFrog.Domain.Resources.Strings" using a shared
///     <see cref="ResourceManager" />. Accessors are culture-aware and thread-safe.
/// </remarks>
internal static class SR
{
    /// <summary>
    ///     Shared resource manager bound to <c>NumericLeapFrog.Domain.Resources.Strings</c> in this assembly.
    /// </summary>
    private static readonly ResourceManager _rm = new("NumericLeapFrog.Domain.Resources.Strings", typeof(SR).Assembly);

    /// <summary>
    ///     Informational message logged when a new game target is generated.
    /// </summary>
    /// <value>Resource key: <c>LogTargetGenerated</c>.</value>
    public static string LogTargetGenerated => _rm.GetString("LogTargetGenerated")!;

    /// <summary>
    ///     Warning message logged when user input cannot be parsed as an integer.
    /// </summary>
    /// <value>Resource key: <c>LogInvalidInput</c>.</value>
    public static string LogInvalidInput => _rm.GetString("LogInvalidInput")!;

    /// <summary>
    ///     Informational message written when the application is starting up.
    /// </summary>
    /// <value>Resource key: <c>AppStarting</c>.</value>
    public static string AppStarting => _rm.GetString("AppStarting")!;

    /// <summary>
    ///     Informational message written when the application is exiting.
    /// </summary>
    /// <value>Resource key: <c>AppExiting</c>.</value>
    public static string AppExiting => _rm.GetString("AppExiting")!;
}