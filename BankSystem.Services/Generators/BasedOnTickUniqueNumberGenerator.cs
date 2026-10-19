using System.Globalization;
using BankSystem.Services.Helpers;

namespace BankSystem.Services.Generators;

/// <summary>
/// Generates unique numbers based on the time elapsed since a starting point.
/// </summary>
public class BasedOnTickUniqueNumberGenerator : IUniqueNumberGenerator
{
    private readonly DateTime startingPoint;

    /// <summary>
    /// Initializes a new instance of the <see cref="BasedOnTickUniqueNumberGenerator"/> class.
    /// </summary>
    public BasedOnTickUniqueNumberGenerator(DateTime startingPoint)
    {
        this.startingPoint = startingPoint;
    }

    /// <summary>
    /// Generates a unique number as a hash of the elapsed ticks.
    /// </summary>
    public string Generate()
    {
        // Taken on every call, so consecutive calls get different values.
        long elapsedTicks = (DateTime.Now - this.startingPoint).Ticks;
        return elapsedTicks.ToString(CultureInfo.InvariantCulture).GenerateHash();
    }
}
