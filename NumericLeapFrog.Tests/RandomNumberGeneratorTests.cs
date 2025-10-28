#region

using NumericLeapFrog.Infrastructure.Randomness;

#endregion

namespace NumericLeapFrog.Tests;

public class RandomNumberGeneratorTests
{
    [Fact]
    public void NextInclusive_ReturnsWithinRange()
    {
        var rng = new RandomNumberGenerator();
        for (var i = 0; i < 100; i++)
        {
            var v = rng.NextInclusive(1, 5);
            Assert.InRange(v, 1, 5);
        }
    }

    [Fact]
    public void NextInclusive_Throws_WhenMaxLessThanMin()
    {
        var rng = new RandomNumberGenerator();
        Assert.Throws<ArgumentOutOfRangeException>(() => rng.NextInclusive(5, 1));
    }
}