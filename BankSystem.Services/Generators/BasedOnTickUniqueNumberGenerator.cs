using System.Globalization;
using BankSystem.Services.Helpers;

namespace BankSystem.Services.Generators;

/// <summary>
/// Generates unique numbers based on the time elapsed since a starting point.
/// </summary>
public class BasedOnTickUniqueNumberGenerator : IUniqueNumberGenerator
{
    private readonly DateTime startingPoint;

    public BasedOnTickUniqueNumberGenerator(DateTime startingPoint)
    {
        this.startingPoint = startingPoint;
    }

    // Generates a unique number as a hash of the elapsed ticks.
    public string Generate()
    {
        // Taken on every call, so consecutive calls get different values.
        long elapsedTicks = (DateTime.Now - this.startingPoint).Ticks;
        string answer = elapsedTicks.ToString(CultureInfo.InvariantCulture).GenerateHash();

        return answer;
    }
}
