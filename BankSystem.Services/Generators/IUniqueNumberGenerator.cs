namespace BankSystem.Services.Generators;

/// <summary>
/// Defines a generator of unique account numbers.
/// </summary>
public interface IUniqueNumberGenerator
{
    /// <summary>
    /// Generates a string that represents a unique number.
    /// </summary>
    string Generate();
}
