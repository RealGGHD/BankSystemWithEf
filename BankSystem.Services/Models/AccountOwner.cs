using BankSystem.Services.Helpers;
using BankSystem.Services.Models.Accounts;

namespace BankSystem.Services.Models;

/// <summary>
/// Represents a bank account owner.
/// </summary>
public class AccountOwner
{
    private readonly List<BankAccount> accounts = new();

    public AccountOwner(string firstName, string lastName, string email)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
    }

    public string FirstName
    {
        get;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("First name is required.", nameof(value));
            }

            field = value;
        }
    }

    public string LastName
    {
        get;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Last name is required.", nameof(value));
            }

            field = value;
        }
    }

    public string Email
    {
        get;
        set
        {
            if (!value.IsEmailValid())
            {
                throw new ArgumentException("Email is not valid.", nameof(value));
            }

            field = value;
        }
    }

    // Returns the owner's full name and email.
    public override string ToString()
    {
        return $"{this.FirstName} {this.LastName}, {this.Email}.";
    }

    // Adds an account to the owner's accounts.
    public void Add(BankAccount account)
    {
        ArgumentNullException.ThrowIfNull(account);

        // No duplicate check: the account constructor already registers itself.
        this.accounts.Add(account);
    }

    // Returns the owner's accounts.
    public IList<BankAccount> Accounts()
    {
        return this.accounts;
    }
}
