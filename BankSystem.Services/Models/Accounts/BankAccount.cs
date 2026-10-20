using System.Globalization;
using BankSystem.Services.Generators;
using BankSystem.Services.Helpers;

namespace BankSystem.Services.Models.Accounts;

/// <summary>
/// Represents a base bank account.
/// </summary>
public abstract class BankAccount
{
    private readonly List<AccountCashOperation> operations = new();

    protected BankAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator)
    {
        // Main constructor: all other constructors end up here.
        ArgumentNullException.ThrowIfNull(owner);
        ArgumentNullException.ThrowIfNull(numberGenerator);

        if (string.IsNullOrWhiteSpace(currencyCode) || !currencyCode.IsCurrencyValid())
        {
            throw new ArgumentException("Currency code is invalid.", nameof(currencyCode));
        }

        this.AccountOwner = owner;
        this.CurrencyCode = currencyCode;
        this.Number = numberGenerator();

        // Register only after all checks pass.
        owner.Add(this);
    }

    protected BankAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator)
        // Turns the generator object into a function and passes it to the main constructor.
        : this(owner, currencyCode, ToNumberGenerator(uniqueNumberGenerator))
    {
    }

    protected BankAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator, decimal initialBalance)
        // Check the balance before the main constructor registers the account at the owner.
        : this(owner, currencyCode, EnsurePositiveInitialBalance(initialBalance, numberGenerator))
    {
        // The initial balance is a regular deposit, so it is recorded and earns bonus points.
        this.Deposit(initialBalance, DateTime.Now, "Initial balance");
    }

    protected BankAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator, decimal initialBalance)
        // Same conversion, then the constructor above checks and deposits the initial balance.
        : this(owner, currencyCode, ToNumberGenerator(uniqueNumberGenerator), initialBalance)
    {
    }

    public string Number { get; }

    public decimal Balance { get; private set; }

    public string CurrencyCode { get; }

    public AccountOwner AccountOwner { get; }

    public int BonusPoints { get; private set; }

    protected abstract decimal Overdraft { get; }

    // Returns all operations performed on the account.
    public IList<AccountCashOperation> GetAllOperations()
    {
        return this.operations;
    }

    // Deposits money to the account.
    public void Deposit(decimal amount, DateTime date, string note)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);
        this.Balance += amount;
        this.operations.Add(new AccountCashOperation(amount, date, note));

        // Bonus points use the balance after the deposit.
        this.BonusPoints += this.CalculateDepositRewardPoints(amount);
    }

    // Withdraws money from the account.
    public void Withdraw(decimal amount, DateTime date, string note)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);

        // Overdraft is a limit, so the balance may become negative.
        if (amount > this.Balance + this.Overdraft)
        {
            throw new InvalidOperationException("Insufficient funds.");
        }

        this.Balance -= amount;

        // Negative amount marks the operation as a debit.
        this.operations.Add(new AccountCashOperation(-amount, date, note));
        this.BonusPoints += this.CalculateWithdrawRewardPoints(amount);
    }

    // Returns a text description of the account.
    public override string ToString()
    {
        string answer = string.Create(
            CultureInfo.InvariantCulture,
            $"{this.Overdraft} {this.AccountOwner} No:{this.Number}. Balance: {this.Balance}{this.CurrencyCode}.\n");

        return answer;
    }

    protected abstract int CalculateDepositRewardPoints(decimal amount);

    protected abstract int CalculateWithdrawRewardPoints(decimal amount);

    // Runs inside the constructor chain, so the null check happens before .Generate is accessed.
    private static Func<string> ToNumberGenerator(IUniqueNumberGenerator uniqueNumberGenerator)
    {
        ArgumentNullException.ThrowIfNull(uniqueNumberGenerator);

        return uniqueNumberGenerator.Generate;
    }

    // Runs inside the constructor chain, so an invalid balance fails before anything is changed.
    private static Func<string> EnsurePositiveInitialBalance(decimal initialBalance, Func<string> numberGenerator)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(initialBalance);

        return numberGenerator;
    }
}
