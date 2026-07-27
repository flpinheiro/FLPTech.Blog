using FLPTech.Blog.Domain.Services.Repositories;
using FLPTech.Blog.Infraestructure.Contexts;
using FLPTech.Blog.Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace FLPTech.Blog.Infraestructure;

/// <summary>
/// Unit of work implementation for managing repositories, database transactions and other related operations.
/// </summary>
internal class UnitOfWork: IUnitOfWork
{
    private readonly AppDbContext _context;
    private bool _disposed = false;
    private IDbContextTransaction? _currentTransaction;
    public UnitOfWork(AppDbContext dbContext)
    {
        _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    private IArticleRepository? _articleRepository;
    public IArticleRepository Articles => _articleRepository ??= new ArticleRepository(_context);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction != null)
        {
            throw new InvalidOperationException("A transaction is already in progress.");
        }

        _currentTransaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction == null)
        {
            throw new InvalidOperationException("No transaction has been started.");
        }

        try
        {
            await _context.SaveChangesAsync(cancellationToken);
            await _currentTransaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            await DisposeTransactionAsync();
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction == null) return;

        try
        {
            await _currentTransaction.RollbackAsync(cancellationToken);
        }
        finally
        {
            await DisposeTransactionAsync();

            // CRITICAL: Clear the internal EF tracking cache.
            // This prevents failed/corrupted entities from persisting if the context is reused.
            _context.ChangeTracker.Clear();
        }
    }

    private async Task DisposeTransactionAsync()
    {
        if (_currentTransaction != null)
        {
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed) return;

        try
        {
            // If a transaction was started but never committed, this drops it safely at the DB level.
            await DisposeTransactionAsync();
            await _context.DisposeAsync();
        }
        finally
        {
            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
