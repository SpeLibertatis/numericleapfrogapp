namespace NumericLeapFrog.Domain.BusinessLogic;

/// <summary>
///     <see cref="IRandomNumberGenerator" /> implementation backed by <see cref="System.Random" />.
/// </summary>
/// <param name="random">Optional random source; when null, a new <see cref="System.Random" /> is created.</param>
/// <remarks>
///     Generates integers in an inclusive range. Instances of <see cref="System.Random" /> are not thread-safe;
///     synchronize access if sharing across threads.
/// </remarks>
public sealed class RandomNumberGenerator(Random? random = null) : IRandomNumberGenerator
{
    private readonly Random _random = random ?? new Random();

    /// <summary>
    ///     Returns a random integer within the specified inclusive range.
    /// </summary>
    /// <param name="minInclusive">The inclusive lower bound.</param>
    /// <param name="maxInclusive">The inclusive upper bound.</param>
    /// <returns>A random integer in the range [<paramref name="minInclusive" />, <paramref name="maxInclusive" />].</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown when <paramref name="minInclusive" /> is greater than
    ///     <paramref name="maxInclusive" />.
    /// </exception>
    /// <exception cref="OverflowException">Thrown when <paramref name="maxInclusive" /> is <see cref="int.MaxValue" />.</exception>
    /// <remarks>
    ///     Implemented via <see cref="Random.Next(int, int)" /> using an exclusive upper bound of <c>maxInclusive +1</c> in a
    ///     checked context.
    /// </remarks>
    public int Next(int minInclusive, int maxInclusive)
    {
        return _random.Next(minInclusive, checked(maxInclusive + 1));
    }
}