using BankSystem.Services.Helpers;

namespace BankSystem.Services.Generators;

/// <summary>
/// Generates unique numbers based on a GUID.
/// </summary>
public class GuidNumberGenerator : IUniqueNumberGenerator
{
    // Generates a unique number as a hash of a new GUID.
    public string Generate()
    {
        string answer = Guid.NewGuid().ToString().GenerateHash();

        return answer;
    }
}
