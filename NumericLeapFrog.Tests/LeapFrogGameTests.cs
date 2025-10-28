using NumericLeapFrog.Domain.BusinessLogic;
using NumericLeapFrog.Domain.Models;

namespace NumericLeapFrog.Tests;

public class LeapFrogGameTests
{
    [Fact]
    public void ApplyGuess_Win_WhenWithinThreshold()
    {
        // target50, threshold default3
        var game = new LeapFrogGame(50);
        var r1 = game.ApplyGuess(47); // diff =3
        Assert.Equal(GameOutcome.Win, r1.Outcome);
        Assert.Equal(50 -47, r1.Difference);
    }

    [Fact]
    public void ApplyGuess_Lose_WhenOverTarget()
    {
        var game = new LeapFrogGame(30);
        game.ApplyGuess(20);
        var r2 = game.ApplyGuess(15); // total35 >30
        Assert.Equal(GameOutcome.Lose, r2.Outcome);
        Assert.True(r2.Total > r2.Target);
    }

    [Fact]
    public void ApplyGuess_Continue_WhenUnderAndOutsideThreshold()
    {
        var game = new LeapFrogGame(100);
        var r1 = game.ApplyGuess(90); // diff =10
        Assert.Equal(GameOutcome.Continue, r1.Outcome);
        Assert.Equal(10, r1.Difference);
    }
}