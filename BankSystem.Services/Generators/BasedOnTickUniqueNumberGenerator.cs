namespace BankSystem.Services.Generators;

public class BasedOnTickUniqueNumberGenerator : IUniqueNumberGenerator
{
    private readonly DateTime startingPoint;

    public BasedOnTickUniqueNumberGenerator(DateTime startingPoint)
    {
        this.startingPoint = startingPoint;
    }

    public string Generate()
    {
        throw new NotImplementedException();
    }
}
