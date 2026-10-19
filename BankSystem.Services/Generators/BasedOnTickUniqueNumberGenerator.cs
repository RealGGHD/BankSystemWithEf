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
        // TODO: hash the ticks elapsed from startingPoint to now (CryptoHelper.GenerateHash).
        throw new NotImplementedException();
    }
}
