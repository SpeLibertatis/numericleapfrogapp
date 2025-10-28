using Microsoft.Extensions.Logging;
using NumericLeapFrog.Domain.Models;
using NumericLeapFrog.UI;

namespace NumericLeapFrog.Domain.BusinessLogic;

public sealed class GameRunner : IGameRunner
{
 private readonly IGameUI _ui;
 private readonly IRandomNumberGenerator _rng;
 private readonly GameOptions _options;
 private readonly ILogger _logger;

 public GameRunner(IGameUI ui, IRandomNumberGenerator rng, GameOptions options, ILogger logger)
 {
 _ui = ui;
 _rng = rng;
 _options = options;
 _logger = logger;
 }

 public void Run()
 {
 var target = _rng.Next(_options.TargetMin, _options.TargetMax);
 var game = new LeapFrogGame(target, _options);
 _logger.LogInformation("Target generated: {Target}", target);

 _ui.ShowGreeting();
 _ui.ShowInstructions();
 RunGameLoop(game);
 }

 private void RunGameLoop(LeapFrogGame game)
 {
 while (true)
 {
 var (ok, guess) = _ui.PromptGuess();
 if (!ok)
 {
 _logger.LogWarning("Invalid input provided by user");
 continue;
 }

 _logger.LogInformation("User guess received: {Guess}", guess);
 var result = game.ApplyGuess(guess);
 _logger.LogDebug("Outcome: {Outcome}, Total: {Total}, Target: {Target}", result.Outcome, result.Total, result.Target);

 switch (result.Outcome)
 {
 case GameOutcome.Win:
 _ui.ShowWin();
 _logger.LogInformation("Game finished with outcome {Outcome}", result.Outcome);
 return;
 case GameOutcome.Lose:
 _ui.ShowLoss();
 _logger.LogInformation("Game finished with outcome {Outcome}", result.Outcome);
 return;
 case GameOutcome.Continue:
 default:
 _ui.ShowContinue(result.Total);
 break;
 }
 }
 }
}
