using NumericLeapFrog.Infrastructure.Abstractions;

namespace NumericLeapFrog.UI;

public sealed class ConsoleGameUI(IConsole console, Typewriter typer, IStrings strings) : IGameUI
{
 private readonly IConsole _console = console;
 private readonly Typewriter _typer = typer;
 private readonly IStrings _strings = strings;

 public void ShowGreeting() => _typer.TypeWriteLine(_strings.Welcome);

 public void ShowInstructions()
 {
 _typer.TypeWriteLine(_strings.Instructions);
 _console.WriteLine(string.Empty);
 }

 public (bool ok, int guess) PromptGuess()
 {
 _typer.TypeWrite(_strings.Prompt);
 var input = _console.ReadLine();
 if (!int.TryParse(input, out var value))
 {
 _typer.TypeWriteLine(_strings.InvalidNumber);
 return (false,0);
 }
 return (true, value);
 }

 public void ShowContinue(int total)
 {
 _typer.TypeWriteLine(_strings.ContinueMessage);
 _typer.TypeWriteLine(_strings.ContinuePrompt);
 PauseAndClear();
 _typer.TypeWriteLine(string.Format(_strings.TotalSoFarFormat, total));
 }

 public void ShowWin()
 {
 _console.WriteLine(string.Empty);
 _typer.TypeWriteLine(_strings.Win);
 }

 public void ShowLoss()
 {
 _console.WriteLine(string.Empty);
 _typer.TypeWriteLine(_strings.Loss);
 }

 public void PauseAndClear()
 {
 _console.ReadLine();
 _console.Clear();
 }
}
