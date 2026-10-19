using BankSystem.Services.Generators;

namespace BankSystem.Services.Models.Accounts;

/// <summary>
/// Represents a gold bank account with an overdraft based on bonus points.
/// </summary>
public class GoldAccount : BankAccount
{
    private const int GoldDepositCostPerPoint = 10;
    private const int GoldWithdrawCostPerPoint = 5;
    private const int GoldBalanceCostPerPoint = 5;

    /// <summary>
    /// Initializes a new instance of the <see cref="GoldAccount"/> class.
    /// </summary>
    public GoldAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator)
        : base(owner, currencyCode, uniqueNumberGenerator)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GoldAccount"/> class.
    /// </summary>
    public GoldAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator)
        : base(owner, currencyCode, numberGenerator)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GoldAccount"/> class with an initial balance.
    /// </summary>
    public GoldAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator, decimal initialBalance)
        : base(owner, currencyCode, uniqueNumberGenerator, initialBalance)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GoldAccount"/> class with an initial balance.
    /// </summary>
    public GoldAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator, decimal initialBalance)
        : base(owner, currencyCode, numberGenerator, initialBalance)
    {
    }

    /// <summary>
    /// Gets the overdraft limit. Equals 3 * BonusPoints.
    /// </summary>
    protected override decimal Overdraft => 3 * this.BonusPoints;

    /// <summary>
    /// Calculates bonus points for a deposit based on the balance and the deposit amount, rounded up.
    /// </summary>
    protected override int CalculateDepositRewardPoints(decimal amount)
        => (int)Math.Max(Math.Ceiling((this.Balance / GoldBalanceCostPerPoint) + (amount / GoldDepositCostPerPoint)), 0);

    /// <summary>
    /// Calculates bonus points for a withdrawal based on the balance and the withdrawal amount, rounded up.
    /// </summary>
    protected override int CalculateWithdrawRewardPoints(decimal amount)
        => (int)Math.Max(Math.Ceiling((this.Balance / GoldBalanceCostPerPoint) + (amount / GoldWithdrawCostPerPoint)), 0);
}
