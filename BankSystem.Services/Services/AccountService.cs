using BankSystem.EF.Entities;
using BankSystem.Services.Models;

namespace BankSystem.Services.Services;

/// <summary>
/// Builds reports about bank accounts.
/// </summary>
public class AccountService : BaseService
{
    public AccountService(BankContext bankContext)
        : base(bankContext)
    {
    }

    // Returns one row per bank account with its owner's name and currency code.
    public IReadOnlyList<BankAccountFullInfoModel> GetBankAccountsFullInfo()
    {
        // Navigations (AccountOwner, CurrencyCode) are turned into SQL joins by EF.
        List<BankAccountFullInfoModel> answer = this.Context.BankAccounts
            .Select(account => new BankAccountFullInfoModel
            {
                BankAccountId = account.Id,
                FirstName = account.AccountOwner.FirstName,
                LastName = account.AccountOwner.LastName,
                AccountNumber = account.Number,
                Balance = account.Balance,
                CurrencyCode = account.CurrencyCode.CurrenciesCode,
                BonusPoints = account.BonusPoints,
            })
            .ToList();

        return answer;
    }
}
