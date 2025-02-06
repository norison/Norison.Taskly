using Microsoft.EntityFrameworkCore;

using Norison.Taskly.Todos.Domain.Repositories;

namespace Norison.Taskly.Todos.Persistence.Repositories;

public class GenericRepository<T>(TodosDbContext dbContext) : IRepository<T> where T : class
{
    public async Task<int> GetCountAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        return await dbContext.Set<T>().CountAsync(cancellationToken);
    }

    public async Task<T?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Set<T>().FindAsync([id], cancellationToken: cancellationToken);
    }

    public async Task<T[]> GetAllAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Set<T>().ToArrayAsync(cancellationToken);
    }

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
    {
        await dbContext.Set<T>().AddAsync(entity, cancellationToken);
        return entity;
    }

    public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        dbContext.Set<T>().Update(entity);
        await Task.CompletedTask;
        return entity;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await GetAsync(id, cancellationToken);
        
        if (entity is not null)
        {
            dbContext.Set<T>().Remove(entity);
        }
    }
}