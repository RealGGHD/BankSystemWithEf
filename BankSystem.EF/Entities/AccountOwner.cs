using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankSystem.EF.Entities;

/// <summary>
/// Represents a row of the account_owner table.
/// </summary>
[Table("account_owner")]
public class AccountOwner
{
    [Key]
    [Column("account_owner_id")]
    public int Id { get; set; }

    [Column("first_name")]
    public string FirstName { get; set; }

    [Column("last_name")]
    public string LastName { get; set; }

    [Column("email")]
    public string Email { get; set; }

    // Navigation to all accounts of this owner (not a column).
    public IList<BankAccount> BankAccounts { get; set; }
}
