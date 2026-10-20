using Microsoft.EntityFrameworkCore;

namespace BankSystem.EF.Entities;

/// <summary>
/// Represents a session with the bank system database.
/// </summary>
public class BankContext : DbContext
{
    // The database provider and connection come from outside (e.g. SQLite in tests).
    public BankContext(DbContextOptions options)
        : base(options)
    {
    }

    // One DbSet per table.
    public DbSet<BankAccount> BankAccounts { get; set; }

    public DbSet<AccountOwner> AccountOwners { get; set; }

    public DbSet<CurrencyCode> CurrencyCodes { get; set; }

    public DbSet<AccountCashOperation> AccountCashOperations { get; set; }
}
