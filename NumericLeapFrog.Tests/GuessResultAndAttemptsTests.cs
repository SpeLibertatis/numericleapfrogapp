using System.Reflection;
using NumericLeapFrog.Configuration.Options;
using NumericLeapFrog.Domain.BusinessLogic;
using NumericLeapFrog.Domain.Models;

namespace NumericLeapFrog.Tests;

public class GuessResultAndAttemptsTests
{
    [Fact]
    public void Attempts_Increment_WithEach_Guess()
    {
        var game = new LeapFrogGame(20, new GameOptions());

        var r1 = game.ApplyGuess(5);
        Assert.Equal(1, r1.Attempts);

        var r2 = game.ApplyGuess(5);
        Assert.Equal(2, r2.Attempts);

        var r3 = game.ApplyGuess(5);
        Assert.Equal(3, r3.Attempts);
    }

    [Fact]
    public void GuessResult_DoesNotExpose_Target_Property()
    {
        var props = typeof(GuessResult).GetProperties();
        Assert.DoesNotContain(props, p => p.Name == "Target");

        var fields =
            typeof(GuessResult).GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
        Assert.DoesNotContain(fields, f => f.Name == "Target");
    }
}