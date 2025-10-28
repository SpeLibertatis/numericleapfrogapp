namespace NumericLeapFrog.UI;

public interface IGameUI
{
 void ShowGreeting();
 void ShowInstructions();
 (bool ok, int guess) PromptGuess();
 void ShowContinue(int total);
 void ShowWin();
 void ShowLoss();
 void PauseAndClear();
}
