using System.Globalization;
using BankSystem.Services.Helpers;

namespace BankSystem.Services.Generators;

/// <summary>
/// Generates unique numbers based on the time elapsed since a starting point.
/// </summary>
public class BasedOnTickUniqueNumberGenerator : IUniqueNumberGenerator
{
    private readonly DateTime startingPoint;

    // Last issued tick value; shared by all threads that use this generator.
    private long lastTicks = long.MinValue;

    public BasedOnTickUniqueNumberGenerator(DateTime startingPoint)
    {
        this.startingPoint = startingPoint;
    }

    // Generates a unique number as a hash of the elapsed ticks.
    public string Generate()
    {
        // Taken on every call, so consecutive calls get different values.
        long elapsedTicks = (DateTime.Now - this.startingPoint).Ticks;
        long previous;
        long next;

        // Two calls within the same tick would collide, so each value must be greater than the last one.
        // CompareExchange saves it only if no other thread changed lastTicks in between; otherwise retry.
        do
        {
            previous = Interlocked.Read(ref this.lastTicks);
            next = Math.Max(elapsedTicks, previous + 1);
        }
        while (Interlocked.CompareExchange(ref this.lastTicks, next, previous) != previous);

        string answer = next.ToString(CultureInfo.InvariantCulture).GenerateHash();

        return answer;
    }
}
