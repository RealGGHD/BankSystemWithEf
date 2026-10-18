using BankSystem.Services.Models.Accounts;

namespace BankSystem.Services.Models;
/// <summary>
/// Represents a bank account owner.
/// </summary>
public class AccountOwner
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AccountOwner"/> class.
    /// </summary>
    public AccountOwner(string firstName, string lastName, string email)
    {
        // Assigning through properties so the same validation runs here.
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
    }

    /// <summary>
    /// Gets or sets the first name of the owner.
    /// </summary>
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

    /// <summary>
    /// Gets or sets the last name of the owner.
    /// </summary>
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

    /// <summary>
    /// Gets or sets the email address of the owner.
    /// </summary>
    public string Email
    {
        get;
        set
        {
            // TODO: throw ArgumentException when the email is not valid (ValidationService.IsEmailValid).
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Returns the owner's full name and email.
    /// </summary>
    public override string ToString()
    {
        return $"{this.FirstName} {this.LastName}, {this.Email}.";
    }

    /// <summary>
    /// Adds an account to the owner's accounts.
    /// </summary>
    public void Add(BankAccount account)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Returns the owner's accounts.
    /// </summary>
    public IList<BankAccount> Accounts()
    {
        throw new NotImplementedException();
    }
}
