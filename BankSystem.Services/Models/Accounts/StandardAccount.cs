using BankSystem.Services.Generators;

namespace BankSystem.Services.Models.Accounts;

/// <summary>
/// Represents a standard bank account without overdraft.
/// </summary>
public class StandardAccount : BankAccount
{
    private const int StandardBalanceCostPerPoint = 100;

    public StandardAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator)
        : base(owner, currencyCode, uniqueNumberGenerator)
    {
    }

    public StandardAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator)
        : base(owner, currencyCode, numberGenerator)
    {
    }

    public StandardAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator, decimal initialBalance)
        : base(owner, currencyCode, uniqueNumberGenerator, initialBalance)
    {
    }

    public StandardAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator, decimal initialBalance)
        : base(owner, currencyCode, numberGenerator, initialBalance)
    {
    }

    // Gets the overdraft limit. Always 0 for a standard account.
    protected override decimal Overdraft => 0;

    protected override int CalculateDepositRewardPoints(decimal amount)
    {
        return this.CalculateBalancePoints();
    }

    protected override int CalculateWithdrawRewardPoints(decimal amount)
    {
        return this.CalculateBalancePoints();
    }

    // Standard account points depend only on the balance.
    private int CalculateBalancePoints()
    {
        decimal answer = Math.Max(Math.Floor(this.Balance / StandardBalanceCostPerPoint), 0);

        return (int)answer;
    }
}
