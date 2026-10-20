namespace BankSystem.Services.Models;

/// <summary>
/// Represents full information about one bank account, its owner and currency.
/// </summary>
public class BankAccountFullInfoModel
{
    public int BankAccountId { get; set; }

    // Owner's name, taken from the related account_owner row.
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string AccountNumber { get; set; }

    public decimal Balance { get; set; }

    // ISO code, taken from the related currency_code row.
    public string CurrencyCode { get; set; }

    public int BonusPoints { get; set; }
}
