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

    public GoldAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator)
        : base(owner, currencyCode, uniqueNumberGenerator)
    {
    }

    public GoldAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator)
        : base(owner, currencyCode, numberGenerator)
    {
    }

    public GoldAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator, decimal initialBalance)
        : base(owner, currencyCode, uniqueNumberGenerator, initialBalance)
    {
    }

    public GoldAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator, decimal initialBalance)
        : base(owner, currencyCode, numberGenerator, initialBalance)
    {
    }

    // Gets the overdraft limit. Equals 3 * BonusPoints.
    protected override decimal Overdraft => 3 * this.BonusPoints;

    protected override int CalculateDepositRewardPoints(decimal amount)
    {
        return this.CalculatePoints(amount, GoldDepositCostPerPoint);
    }

    protected override int CalculateWithdrawRewardPoints(decimal amount)
    {
        return this.CalculatePoints(amount, GoldWithdrawCostPerPoint);
    }

    // Gold points depend on the balance and the operation amount, rounded up.
    private int CalculatePoints(decimal amount, int amountCostPerPoint)
    {
        decimal answer = Math.Max(Math.Ceiling((this.Balance / GoldBalanceCostPerPoint) + (amount / amountCostPerPoint)), 0);

        return (int)answer;
    }
}
