using Microsoft.EntityFrameworkCore;

using Norison.Taskly.Todos.Domain.Repositories;

using Sieve.Models;
using Sieve.Services;

namespace Norison.Taskly.Todos.Persistence.Repositories;

public class GenericRepository<T>(TodosDbContext dbContext, ISieveProcessor sieveProcessor)
    : IRepository<T> where T : class
{
    public async Task<T?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Set<T>().FindAsync([id], cancellationToken: cancellationToken);
    }

    public async Task<int> GetCountAsync(ListArgs args, CancellationToken cancellationToken)
    {
        var query = dbContext.Set<T>().AsNoTracking();
        return await ApplySieve(args, query).CountAsync(cancellationToken);
    }

    public async Task<T[]> GetListAsync(ListArgs args, CancellationToken cancellationToken)
    {
        var query = dbContext.Set<T>().AsNoTracking();
        return await ApplySieve(args, query).ToArrayAsync(cancellationToken);
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

    private IQueryable<T> ApplySieve(ListArgs args, IQueryable<T> query)
    {
        var sieveModel = new SieveModel
        {
            Page = args.Page, PageSize = args.PageSize, Filters = args.Filters, Sorts = args.Sorts
        };

        return sieveProcessor.Apply(sieveModel, query);
    }
}