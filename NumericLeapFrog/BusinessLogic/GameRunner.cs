using Microsoft.Extensions.Logging;
using NumericLeapFrog.Domain.Models;
using NumericLeapFrog.Infrastructure.Abstractions;
using NumericLeapFrog.Infrastructure.Randomness;
using NumericLeapFrog.UI;
using NumericLeapFrog.UI.Resources;

namespace NumericLeapFrog.Domain.BusinessLogic;

public sealed class GameRunner : IGameRunner
{
 private readonly IConsole _io;
 private readonly Typewriter _typer;
 private readonly LeapFrogGame _game;
 private readonly ILogger _logger;

 public GameRunner(IConsole io, Typewriter typer, LeapFrogGame game, ILogger logger)
 {
 _io = io;
 _typer = typer;
 _game = game;
 _logger = logger;
 }

 public void Run()
 {
 Greeting(_typer);
 Instructions(_typer, _io);
 RunGameLoop(_io, _typer, _game, _logger);
 }

 private static void Greeting(Typewriter typer) => typer.TypeWriteLine(SR.Welcome);

 private static void Instructions(Typewriter typer, IConsole io)
 {
 typer.TypeWriteLine(SR.Instructions1);
 typer.TypeWriteLine(SR.Instructions2);
 io.WriteLine(string.Empty);
 }

 private static void RunGameLoop(IConsole io, Typewriter typer, LeapFrogGame game, ILogger logger)
 {
 while (true)
 {
 typer.TypeWrite(SR.PromptGuess);
 if (!TryReadInt(io, typer, out var guess))
 {
 logger.LogWarning("Invalid input provided by user");
 continue;
 }

 logger.LogInformation("User guess received: {Guess}", guess);
 var result = game.ApplyGuess(guess);
 logger.LogDebug("Outcome: {Outcome}, Total: {Total}, Target: {Target}", result.Outcome, result.Total, result.Target);

 if (HandleOutcome(io, typer, result))
 {
 logger.LogInformation("Game finished with outcome {Outcome}", result.Outcome);
 break;
 }
 }
 }

 private static bool TryReadInt(IConsole io, Typewriter typer, out int value)
 {
 var input = io.ReadLine();
 if (!int.TryParse(input, out value))
 {
 typer.TypeWriteLine(SR.InvalidNumber);
 return false;
 }
 return true;
 }

 private static bool HandleOutcome(IConsole io, Typewriter typer, GuessResult result)
 {
 switch (result.Outcome)
 {
 case GameOutcome.Win:
 HandleWin(io, typer);
 return true;
 case GameOutcome.Lose:
 HandleLoss(io, typer);
 return true;
 case GameOutcome.Continue:
 default:
 HandleContinue(io, typer, result);
 return false;
 }
 }

 private static void HandleContinue(IConsole io, Typewriter typer, GuessResult result)
 {
 typer.TypeWriteLine(SR.ContinueMessage);
 typer.TypeWriteLine(SR.ContinuePrompt);
 PauseAndClear(io);
 typer.TypeWriteLine(string.Format(SR.TotalSoFar, result.Total));
 }

 private static void HandleWin(IConsole io, Typewriter typer)
 {
 io.WriteLine(string.Empty);
 typer.TypeWriteLine(SR.WinMessage);
 }

 private static void HandleLoss(IConsole io, Typewriter typer)
 {
 io.WriteLine(string.Empty);
 typer.TypeWriteLine(SR.LoseMessage);
 }

 private static void PauseAndClear(IConsole io)
 {
 io.ReadLine();
 io.Clear();
 }
}
