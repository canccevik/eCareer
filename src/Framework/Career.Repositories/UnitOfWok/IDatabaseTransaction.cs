using System;
using System.Threading;
using System.Threading.Tasks;

namespace Career.Repositories.UnitOfWok;

public interface IDatabaseTransaction: IDisposable, IAsyncDisposable
{
    void Commit();
    Task CommitAsync(CancellationToken cancellationToken = default);

    void Rollback();
    Task RollbackAsync(CancellationToken cancellationToken = default);
}