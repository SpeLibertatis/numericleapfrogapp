using NumericLeapFrog.UI.Resources;

namespace NumericLeapFrog.UI;

public sealed class ResourceStrings : IStrings
{
 public string Welcome => SR.Welcome;
 public string Instructions1 => SR.Instructions1;
 public string Instructions2 => SR.Instructions2;
 public string PromptGuess => SR.PromptGuess;
 public string InvalidNumber => SR.InvalidNumber;
 public string WinMessage => SR.WinMessage;
 public string LoseMessage => SR.LoseMessage;
 public string ContinueMessage => SR.ContinueMessage;
 public string ContinuePrompt => SR.ContinuePrompt;
 public string TotalSoFarFormat => SR.TotalSoFar;
}
