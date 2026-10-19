using BankSystem.Services.Generators;

namespace BankSystem.Services.Models.Accounts;

/// <summary>
/// Represents a standard bank account without overdraft.
/// </summary>
public class StandardAccount : BankAccount
{
    private const int StandardBalanceCostPerPoint = 100;

    /// <summary>
    /// Initializes a new instance of the <see cref="StandardAccount"/> class.
    /// </summary>
    public StandardAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator)
        : base(owner, currencyCode, uniqueNumberGenerator)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StandardAccount"/> class.
    /// </summary>
    public StandardAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator)
        : base(owner, currencyCode, numberGenerator)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StandardAccount"/> class with an initial balance.
    /// </summary>
    public StandardAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator, decimal initialBalance)
        : base(owner, currencyCode, uniqueNumberGenerator, initialBalance)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StandardAccount"/> class with an initial balance.
    /// </summary>
    public StandardAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator, decimal initialBalance)
        : base(owner, currencyCode, numberGenerator, initialBalance)
    {
    }

    /// <summary>
    /// Gets the overdraft limit. Always 0 for a standard account.
    /// </summary>
    protected override decimal Overdraft => 0;

    /// <summary>
    /// Calculates bonus points for a deposit based on the balance only.
    /// </summary>
    protected override int CalculateDepositRewardPoints(decimal amount)
        => (int)Math.Max(Math.Floor(this.Balance / StandardBalanceCostPerPoint), 0);

    /// <summary>
    /// Calculates bonus points for a withdrawal based on the balance only.
    /// </summary>
    protected override int CalculateWithdrawRewardPoints(decimal amount)
        => (int)Math.Max(Math.Floor(this.Balance / StandardBalanceCostPerPoint), 0);
}
