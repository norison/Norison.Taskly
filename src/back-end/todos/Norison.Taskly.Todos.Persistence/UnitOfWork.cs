using System.Data;

using Microsoft.EntityFrameworkCore.Storage;

using Norison.Taskly.Todos.Application.Interfaces;

namespace Norison.Taskly.Todos.Persistence;

public class UnitOfWork(TodosDbContext dbContext) : IUnitOfWork
{
    public async Task<IDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
    {
        var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
        return transaction.GetDbTransaction();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}