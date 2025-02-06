using System.Data;

namespace Norison.Taskly.Todos.Application.Interfaces;

public interface IUnitOfWork
{
    Task<IDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}