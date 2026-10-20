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

    public SilverAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator)
        : base(owner, currencyCode, uniqueNumberGenerator)
    {
    }

    public SilverAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator)
        : base(owner, currencyCode, numberGenerator)
    {
    }

    public SilverAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator, decimal initialBalance)
        : base(owner, currencyCode, uniqueNumberGenerator, initialBalance)
    {
    }

    public SilverAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator, decimal initialBalance)
        : base(owner, currencyCode, numberGenerator, initialBalance)
    {
    }

    // Gets the overdraft limit. Equals 2 * BonusPoints.
    protected override decimal Overdraft => 2 * this.BonusPoints;

    protected override int CalculateDepositRewardPoints(decimal amount)
    {
        return this.CalculatePoints(amount, SilverDepositCostPerPoint);
    }

    protected override int CalculateWithdrawRewardPoints(decimal amount)
    {
        return this.CalculatePoints(amount, SilverWithdrawCostPerPoint);
    }

    // Silver points depend on the balance and the operation amount.
    private int CalculatePoints(decimal amount, int amountCostPerPoint)
    {
        decimal answer = Math.Max(Math.Floor((this.Balance / SilverBalanceCostPerPoint) + (amount / amountCostPerPoint)), 0);

        return (int)answer;
    }
}
