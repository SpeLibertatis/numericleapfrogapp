using NumericLeapFrog.Infrastructure.Abstractions;

namespace NumericLeapFrog.Infrastructure.Console;

/// <summary>
/// Console implementation of <see cref="IConsole" /> backed by <see cref="System.Console" />.
/// </summary>
public sealed class SystemConsole : IConsole
{
 /// <inheritdoc />
 public void Write(string text)
 {
 global::System.Console.Write(text);
 }

 /// <inheritdoc />
 public void WriteLine(string text)
 {
 global::System.Console.WriteLine(text);
 }

 /// <inheritdoc />
 public string? ReadLine()
 {
 return global::System.Console.ReadLine();
 }

 /// <inheritdoc />
 public void Clear()
 {
 global::System.Console.Clear();
 }
}