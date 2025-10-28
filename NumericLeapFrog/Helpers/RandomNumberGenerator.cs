namespace NumericLeapFrog.Helpers;

/// <summary>
///     Pseudorandom number generator implementation using <see cref="Random" />.
/// </summary>
public sealed class RandomNumberGenerator : IRandomNumberGenerator
{
    private readonly Random _random = new();

    /// <inheritdoc />
    public int NextInclusive(int minInclusive, int maxInclusive)
    {
        return maxInclusive < minInclusive ? 
            throw new ArgumentOutOfRangeException(nameof(maxInclusive)) :
            _random.Next(minInclusive, checked(maxInclusive + 1));
    }
}