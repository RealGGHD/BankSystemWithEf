using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankSystem.EF.Entities;

/// <summary>
/// Represents a row of the account_cash_operation table.
/// </summary>
[Table("account_cash_operation")]
public class AccountCashOperation
{
    [Key]
    [Column("account_cash_operation_id")]
    public int Id { get; set; }

    [Column("bank_account_id")]
    [ForeignKey(nameof(BankAccount))]
    public int BankAccountId { get; set; }

    [Column("amount")]
    public decimal Amount { get; set; }

    // DATETIME in the database, but mapped to a string by the task.
    [Column("operation_date_time")]
    public string OperationDateTime { get; set; }

    [Column("note")]
    [StringLength(160)]
    public string Note { get; set; }

    // Navigation to the account this operation belongs to.
    public BankAccount BankAccount { get; set; }
}
