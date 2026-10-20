using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankSystem.EF.Entities;

/// <summary>
/// Represents a row of the currency_code table.
/// </summary>
[Table("currency_code")]
public class CurrencyCode
{
    [Key]
    [Column("currency_code_id")]
    public int Id { get; set; }

    // Named differently from the class, because a member cannot share its class name.
    [Column("currency_code")]
    [StringLength(5)]
    public string CurrenciesCode { get; set; }

    // Navigation to all accounts in this currency (not a column).
    public IList<BankAccount> BankAccounts { get; set; }
}
