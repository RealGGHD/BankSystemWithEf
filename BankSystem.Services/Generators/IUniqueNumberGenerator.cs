namespace BankSystem.Services.Generators;

/// <summary>
/// Defines a generator of unique account numbers.
/// </summary>
public interface IUniqueNumberGenerator
{
    // Generates a string that represents a unique number.
    string Generate();
}
