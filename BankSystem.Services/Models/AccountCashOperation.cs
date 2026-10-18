using System.Globalization;

namespace BankSystem.Services.Models;

/// <summary>
/// Represents a cash operation on a bank account.
/// </summary>
public class AccountCashOperation
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AccountCashOperation"/> class.
    /// </summary>
    public AccountCashOperation(decimal amount, DateTime date, string note)
    {
        this.Amount = amount;
        this.Date = date;
        this.Note = note;
    }

    /// <summary>
    /// Gets the amount of the operation.
    /// </summary>
    public decimal Amount { get; }

    /// <summary>
    /// Gets the date and time of the operation.
    /// </summary>
    public DateTime Date { get; }

    /// <summary>
    /// Gets the note of the operation.
    /// </summary>
    public string Note { get; }

    /// <summary>
    /// Returns a text description of the operation.
    /// </summary>
    public override string ToString()
    {
        // Negative amount is a debit; positive or zero is a credit.
        string message = this.Amount < 0 ? "Debited from account" : "Credited to account";

        // Invariant culture keeps "/" in the date and "." in decimals.
        return string.Create(
            CultureInfo.InvariantCulture,
            $"{this.Date:MM/dd/yyyy HH:mm:ss} {this.Note} : {message} {this.Amount}.");
    }
}
