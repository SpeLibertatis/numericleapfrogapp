#region

using NumericLeapFrog.Infrastructure.Abstractions;

#endregion

namespace NumericLeapFrog.Infrastructure.Console;

/// <summary>
///     Console implementation of <see cref="IConsole" /> backed by <see cref="System.Console" />.
/// </summary>
public sealed class SystemConsole : IConsole
{
    /// <inheritdoc />
    public void Write(string text)
    {
        System.Console.Write(text);
    }

    /// <inheritdoc />
    public void WriteLine(string text)
    {
        System.Console.WriteLine(text);
    }

    /// <inheritdoc />
    public string? ReadLine()
    {
        return System.Console.ReadLine();
    }

    /// <inheritdoc />
    public void Clear()
    {
        System.Console.Clear();
    }
}