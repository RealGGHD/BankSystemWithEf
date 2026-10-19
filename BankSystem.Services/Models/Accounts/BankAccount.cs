using System.Globalization;
using BankSystem.Services.Generators;

namespace BankSystem.Services.Models.Accounts;

/// <summary>
/// Represents a base bank account.
/// </summary>
public abstract class BankAccount
{
    private readonly List<AccountCashOperation> operations = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="BankAccount"/> class.
    /// </summary>
    protected BankAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator)
    {
        // Main constructor: all other constructors end up here.
        ArgumentNullException.ThrowIfNull(owner);
        ArgumentNullException.ThrowIfNull(numberGenerator);

        // TODO: also check currencyCode.IsCurrencyValid() (ValidationService).
        if (string.IsNullOrWhiteSpace(currencyCode))
        {
            throw new ArgumentException("Currency code is invalid.", nameof(currencyCode));
        }

        this.AccountOwner = owner;
        this.CurrencyCode = currencyCode;
        this.Number = numberGenerator();

        // Register only after all checks pass.
        owner.Add(this);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BankAccount"/> class.
    /// </summary>
    protected BankAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator)
        // Null check must happen here, before .Generate is accessed.
        : this(owner, currencyCode, (uniqueNumberGenerator ?? throw new ArgumentNullException(nameof(uniqueNumberGenerator))).Generate)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BankAccount"/> class with an initial balance.
    /// </summary>
    protected BankAccount(AccountOwner owner, string currencyCode, Func<string> numberGenerator, decimal initialBalance)
        : this(owner, currencyCode, numberGenerator)
    {
        // The initial balance is a regular deposit, so it is recorded and earns bonus points.
        this.Deposit(initialBalance, DateTime.Now, "Initial balance");
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BankAccount"/> class with an initial balance.
    /// </summary>
    protected BankAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator, decimal initialBalance)
        : this(owner, currencyCode, uniqueNumberGenerator)
    {
        this.Deposit(initialBalance, DateTime.Now, "Initial balance");
    }

    /// <summary>
    /// Gets the account number.
    /// </summary>
    public string Number { get; }

    /// <summary>
    /// Gets the account balance.
    /// </summary>
    public decimal Balance { get; private set; }

    /// <summary>
    /// Gets the ISO currency code of the account.
    /// </summary>
    public string CurrencyCode { get; }

    /// <summary>
    /// Gets the owner of the account.
    /// </summary>
    public AccountOwner AccountOwner { get; }

    /// <summary>
    /// Gets the bonus points of the account.
    /// </summary>
    public int BonusPoints { get; private set; }

    /// <summary>
    /// Gets the overdraft limit of the account.
    /// </summary>
    protected abstract decimal Overdraft { get; }

    /// <summary>
    /// Returns all operations performed on the account.
    /// </summary>
    public IList<AccountCashOperation> GetAllOperations()
    {
        return this.operations;
    }

    /// <summary>
    /// Deposits money to the account.
    /// </summary>
    public void Deposit(decimal amount, DateTime date, string note)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);
        this.Balance += amount;
        this.operations.Add(new AccountCashOperation(amount, date, note));

        // Bonus points use the balance after the deposit.
        this.BonusPoints += this.CalculateDepositRewardPoints(amount);
    }

    /// <summary>
    /// Withdraws money from the account.
    /// </summary>
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

    /// <summary>
    /// Returns a text description of the account.
    /// </summary>
    public override string ToString()
    {
        return string.Create(
            CultureInfo.InvariantCulture,
            $"{this.Overdraft} {this.AccountOwner} No:{this.Number}. Balance: {this.Balance}{this.CurrencyCode}.\n");
    }

    /// <summary>
    /// Calculates bonus points for a deposit.
    /// </summary>
    protected abstract int CalculateDepositRewardPoints(decimal amount);

    /// <summary>
    /// Calculates bonus points for a withdrawal.
    /// </summary>
    protected abstract int CalculateWithdrawRewardPoints(decimal amount);
}
