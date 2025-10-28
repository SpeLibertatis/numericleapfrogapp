namespace NumericLeapFrog.Domain.BusinessLogic;

/// <summary>
///     Defines the contract for running the Numeric Leap Frog game.
/// </summary>
/// <remarks>
///     Implementations coordinate game flow, user interaction, and termination conditions.
/// </remarks>
public interface IGameRunner
{
 /// <summary>
 ///     Executes the game loop until the game completes.
 /// </summary>
 void Run();
}