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

    public static SimpleGenerator Instance { get; }

    private SimpleGenerator()
    {
    }

    static SimpleGenerator()
    {
        Instance = new SimpleGenerator();
    }

    // Generates the next sequential number as a hash.
    public string Generate()
    {
        this.counter += 1;
        string answer = this.counter.ToString(CultureInfo.InvariantCulture).GenerateHash();

        return answer;
    }
}
