using System.Globalization;

namespace BankSystem.Services.Models;

/// <summary>
/// Represents a cash operation on a bank account.
/// </summary>
public class AccountCashOperation
{
    public AccountCashOperation(decimal amount, DateTime date, string note)
    {
        this.Amount = amount;
        this.Date = date;
        this.Note = note;
    }

    public decimal Amount { get; }

    public DateTime Date { get; }

    public string Note { get; }

    // Returns a text description of the operation.
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
