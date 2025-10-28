namespace NumericLeapFrog.Helpers;

/// <summary>
///     Abstraction for generating random numbers.
/// </summary>
public interface IRandomNumberGenerator
{
    /// <summary>
    ///     Returns a random integer within the specified inclusive range.
    /// </summary>
    /// <param name="minInclusive">The inclusive lower bound.</param>
    /// <param name="maxInclusive">The inclusive upper bound.</param>
    /// <returns>A random integer in the range [minInclusive, maxInclusive].</returns>
    int NextInclusive(int minInclusive, int maxInclusive);
}