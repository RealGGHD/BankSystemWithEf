using BankSystem.Services.Helpers;

namespace BankSystem.Services.Generators;

/// <summary>
/// Generates unique numbers based on a GUID.
/// </summary>
public class GuidNumberGenerator : IUniqueNumberGenerator
{
    /// <summary>
    /// Generates a unique number as a hash of a new GUID.
    /// </summary>
    public string Generate()
    {
        return Guid.NewGuid().ToString().GenerateHash();
    }
}
