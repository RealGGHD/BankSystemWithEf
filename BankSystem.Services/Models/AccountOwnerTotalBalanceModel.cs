namespace BankSystem.Services.Models;

/// <summary>
/// Represents the total balance of one owner in one currency.
/// </summary>
public class AccountOwnerTotalBalanceModel
{
    public int AccountOwnerId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string CurrencyCode { get; set; }

    // Sum of the balances of all the owner's accounts in this currency.
    public decimal Total { get; set; }
}
