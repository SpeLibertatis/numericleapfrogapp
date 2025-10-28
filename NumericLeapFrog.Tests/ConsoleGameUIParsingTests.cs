using NumericLeapFrog.Infrastructure.Abstractions;
using NumericLeapFrog.UI;

namespace NumericLeapFrog.Tests;

public class ConsoleGameUIParsingTests
{
 private sealed class BufferConsole : IConsole
 {
 private readonly Queue<string> _inputs;
 public readonly List<string> Writes = new();
 public BufferConsole(params string[] inputs) => _inputs = new Queue<string>(inputs);
 public void Write(string text) => Writes.Add(text);
 public void WriteLine(string text) => Writes.Add(text + "\n");
 public string ReadLine() => _inputs.Count >0 ? _inputs.Dequeue() : string.Empty;
 public void Clear() { }
 }

 private sealed class FakeStrings : IStrings
 {
 public string Welcome => "";
 public string Instructions => "";
 public string Prompt => "Enter:";
 public string InvalidNumber => "bad";
 public string ContinueMessage => "";
 public string ContinuePrompt => "";
 public string TotalSoFarFormat => "{0}";
 public string Win => "";
 public string Loss => "";
 }

 [Fact]
 public void PromptGuess_ReturnsFalse_On_Invalid()
 {
 var io = new BufferConsole("foo");
 var ui = new ConsoleGameUI(io, new Typewriter(io, new NumericLeapFrog.Domain.Models.GameOptions{ TypewriterDelayMs =0 }, new NumericLeapFrog.Infrastructure.Time.ThreadDelay()), new FakeStrings());
 var (ok, _) = ui.PromptGuess();
 Assert.False(ok);
 }

 [Fact]
 public void PromptGuess_ReturnsTrue_With_Value()
 {
 var io = new BufferConsole("42");
 var ui = new ConsoleGameUI(io, new Typewriter(io, new NumericLeapFrog.Domain.Models.GameOptions{ TypewriterDelayMs =0 }, new NumericLeapFrog.Infrastructure.Time.ThreadDelay()), new FakeStrings());
 var (ok, value) = ui.PromptGuess();
 Assert.True(ok);
 Assert.Equal(42, value);
 }
}
