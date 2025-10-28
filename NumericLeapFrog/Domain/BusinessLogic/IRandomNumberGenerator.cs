namespace NumericLeapFrog.Domain.BusinessLogic;

public interface IRandomNumberGenerator
{
 int Next(int minInclusive, int maxInclusive);
}
