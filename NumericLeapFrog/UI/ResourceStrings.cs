using NumericLeapFrog.UI.Resources;

namespace NumericLeapFrog.UI;

public sealed class ResourceStrings : IStrings
{
 public string Welcome => SR.Welcome;
 public string Instructions => string.Join(" ", SR.Instructions1, SR.Instructions2);
 public string Prompt => SR.PromptGuess;
 public string InvalidNumber => SR.InvalidNumber;
 public string ContinueMessage => SR.ContinueMessage;
 public string ContinuePrompt => SR.ContinuePrompt;
 public string TotalSoFarFormat => SR.TotalSoFar;
 public string Win => SR.WinMessage;
 public string Loss => SR.LoseMessage;
}
