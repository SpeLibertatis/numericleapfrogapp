namespace NumericLeapFrog.Domain.BusinessLogic;

/// <summary>
/// Abstraction for generating random integers.
/// </summary>
/// <remarks>
/// Implementations should produce values within inclusive bounds as specified by <see cref="Next(int, int)"/>.
/// </remarks>
public interface IRandomNumberGenerator
{
 /// <summary>
 /// Returns a random integer within the specified inclusive range.
 /// </summary>
 /// <param name="minInclusive">The inclusive lower bound.</param>
 /// <param name="maxInclusive">The inclusive upper bound.</param>
 /// <returns>A random integer in the range [<paramref name="minInclusive"/>, <paramref name="maxInclusive"/>].</returns>
 /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="minInclusive"/> is greater than <paramref name="maxInclusive"/>.</exception>
 int Next(int minInclusive, int maxInclusive);
}