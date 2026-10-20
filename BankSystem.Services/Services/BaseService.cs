using BankSystem.EF.Entities;

namespace BankSystem.Services.Services;

/// <summary>
/// Base class for services that work with the bank system database.
/// </summary>
public class BaseService : IDisposable
{
    private bool disposed;

    public BaseService(BankContext bankContext)
    {
        ArgumentNullException.ThrowIfNull(bankContext);

        this.Context = bankContext;
    }

    // Available only to derived services.
    protected BankContext Context { get; }

    // Releases the database context.
    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    // Standard dispose pattern: derived classes can override it to release their own resources.
    protected virtual void Dispose(bool disposing)
    {
        // Dispose may be called more than once; only the first call does the work.
        if (this.disposed)
        {
            return;
        }

        if (disposing)
        {
            this.Context.Dispose();
        }

        this.disposed = true;
    }
}
