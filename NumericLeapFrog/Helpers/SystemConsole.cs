namespace NumericLeapFrog.Helpers;

/// <summary>
///     Console implementation of <see cref="IConsole" /> backed by <see cref="System.Console" />.
/// </summary>
public sealed class SystemConsole : IConsole
{
    /// <inheritdoc />
    public void Write(string text)
    {
        Console.Write(text);
    }

    /// <inheritdoc />
    public void WriteLine(string text)
    {
        Console.WriteLine(text);
    }

    /// <inheritdoc />
    public string? ReadLine()
    {
        return Console.ReadLine();
    }

    /// <inheritdoc />
    public void Clear()
    {
        Console.Clear();
    }
}