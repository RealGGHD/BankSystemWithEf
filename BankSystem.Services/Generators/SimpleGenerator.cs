using System.Globalization;

namespace BankSystem.Services.Generators;

public class SimpleGenerator : IUniqueNumberGenerator
{
    private int counter = 1234567890;
    public static SimpleGenerator Instance { get; }

    private SimpleGenerator()
    {
    }

    static SimpleGenerator()
    {
        Instance = new SimpleGenerator();
    }

    public string Generate()
    {
        this.counter += 1;
        //this.counter.ToString(CultureInfo.InvariantCulture).GenerateHash();
        // TODO: increment the counter and return its hash (CryptoHelper.GenerateHash).
        throw new NotImplementedException();
    }
}
