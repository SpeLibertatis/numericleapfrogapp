using NumericLeapFrog.Domain.BusinessLogic;
using NumericLeapFrog.Domain.Models;

namespace NumericLeapFrog.Tests;

public class LeapFrogGameTests
{
  [Fact]
  public void ApplyGuess_Win_WhenWithinThreshold()
  {
    var game = new LeapFrogGame(50, new GameOptions { Threshold = 3 });
    var r1 = game.ApplyGuess(47); // diff =3
    Assert.Equal(GuessOutcome.Win, r1.Outcome);
    Assert.Equal(3, r1.Difference);
  }

  [Fact]
  public void ApplyGuess_Lose_WhenOverTarget()
  {
    var game = new LeapFrogGame(30, new GameOptions());
    game.ApplyGuess(20);
    var r2 = game.ApplyGuess(15); // total35 >30
    Assert.Equal(GuessOutcome.Loss, r2.Outcome);
    Assert.True(r2.Total > 30);
  }

  [Fact]
  public void ApplyGuess_Continue_WhenUnderAndOutsideThreshold()
  {
    var game = new LeapFrogGame(100, new GameOptions());
    var r1 = game.ApplyGuess(90); // diff =10
    Assert.Equal(GuessOutcome.Continue, r1.Outcome);
    Assert.Equal(10, r1.Difference);
  }
}