using BankSystem.EF.Entities;
using BankSystem.Services.Models;

namespace BankSystem.Services.Services;

/// <summary>
/// Builds reports about account owners.
/// </summary>
public class OwnerService : BaseService
{
    public OwnerService(BankContext bankContext)
        : base(bankContext)
    {
    }

    // Returns the total balance of each owner in each currency, largest first.
    public IReadOnlyList<AccountOwnerTotalBalanceModel> GetAccountOwnersTotalBalance()
    {
        // One group per owner and currency; Balance stays decimal in Sum, no double conversion.
        List<AccountOwnerTotalBalanceModel> answer = this.Context.BankAccounts
            .GroupBy(account => new
            {
                account.AccountOwnerId,
                account.AccountOwner.FirstName,
                account.AccountOwner.LastName,
                account.CurrencyCode.CurrenciesCode,
            })
            .Select(group => new AccountOwnerTotalBalanceModel
            {
                AccountOwnerId = group.Key.AccountOwnerId,
                FirstName = group.Key.FirstName,
                LastName = group.Key.LastName,
                CurrencyCode = group.Key.CurrenciesCode,
                Total = group.Sum(account => account.Balance),
            })
            .OrderByDescending(model => model.Total)
            .ToList();

        return answer;
    }
}
