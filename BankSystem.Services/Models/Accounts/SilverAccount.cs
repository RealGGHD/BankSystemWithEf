using BankSystem.Services.Generators;

namespace BankSystem.Services.Models.Accounts;

/// <summary>
/// Represents a silver bank account with an overdraft based on bonus points.
/// </summary>
public class SilverAccount : BankAccount
{
    private const int SilverDepositCostPerPoint = 5;
    private const int SilverWithdrawCostPerPoint = 2;
    private const int SilverBalanceCostPerPoint = 100;

    /// <summary>
    /// Initializes a new instance of the <see cref="SilverAccount"/> class.
    /// </summary>
    public SilverAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator)
        : base(owner, currencyCode, uniqueNumberGenerator)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SilverAccount"/> class.
    /// </summary>
    public SilverAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator)
        : base(owner, currencyCode, numberGenerator)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SilverAccount"/> class with an initial balance.
    /// </summary>
    public SilverAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator, decimal initialBalance)
        : base(owner, currencyCode, uniqueNumberGenerator, initialBalance)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SilverAccount"/> class with an initial balance.
    /// </summary>
    public SilverAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator, decimal initialBalance)
        : base(owner, currencyCode, numberGenerator, initialBalance)
    {
    }

    /// <summary>
    /// Gets the overdraft limit. Equals 2 * BonusPoints.
    /// </summary>
    protected override decimal Overdraft => 2 * this.BonusPoints;

    /// <summary>
    /// Calculates bonus points for a deposit based on the balance and the deposit amount.
    /// </summary>
    protected override int CalculateDepositRewardPoints(decimal amount)
        => (int)Math.Max(Math.Floor((this.Balance / SilverBalanceCostPerPoint) + (amount / SilverDepositCostPerPoint)), 0);

    /// <summary>
    /// Calculates bonus points for a withdrawal based on the balance and the withdrawal amount.
    /// </summary>
    protected override int CalculateWithdrawRewardPoints(decimal amount)
        => (int)Math.Max(Math.Floor((this.Balance / SilverBalanceCostPerPoint) + (amount / SilverWithdrawCostPerPoint)), 0);
}
