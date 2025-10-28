namespace NumericLeapFrog.Domain.BusinessLogic;

public sealed class RandomNumberGenerator : IRandomNumberGenerator
{
 private readonly System.Random _random;
 public RandomNumberGenerator(System.Random? random = null) => _random = random ?? new System.Random();
 public int Next(int minInclusive, int maxInclusive) => _random.Next(minInclusive, checked(maxInclusive +1));
}
