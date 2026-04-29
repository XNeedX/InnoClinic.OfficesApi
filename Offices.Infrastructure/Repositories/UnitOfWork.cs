using Offices.Application.Abstractions;
using Offices.Infrastructure.Data;

namespace Offices.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly MongoContext _context;

    public UnitOfWork(MongoContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        if (_context.Session != null && _context.Session.IsInTransaction)
        {
            await _context.Session.CommitTransactionAsync(cancellationToken);
        }
    }

    public void Dispose()
    {
        if (_context.Session != null)
        {
            _context.Session.Dispose();
            _context.Session = null;
        }
    }
}