using System.Globalization;
using BankSystem.Services.Helpers;

namespace BankSystem.Services.Generators;

/// <summary>
/// Generates sequential unique numbers. Implemented as a singleton.
/// </summary>
public class SimpleGenerator : IUniqueNumberGenerator
{
    // Last issued number; the first call returns the hash of 1234567891.
    private int counter = 1234567890;

    /// <summary>
    /// Gets the single instance of the generator.
    /// </summary>
    public static SimpleGenerator Instance { get; }

    // Private so that no other instance can be created.
    private SimpleGenerator()
    {
    }

    // Runs once, before the first use of the class.
    static SimpleGenerator()
    {
        Instance = new SimpleGenerator();
    }

    /// <summary>
    /// Generates the next sequential number as a hash.
    /// </summary>
    public string Generate()
    {
        this.counter += 1;
        return this.counter.ToString(CultureInfo.InvariantCulture).GenerateHash();
    }
}
