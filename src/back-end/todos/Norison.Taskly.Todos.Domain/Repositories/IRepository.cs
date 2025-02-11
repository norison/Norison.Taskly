namespace Norison.Taskly.Todos.Domain.Repositories;

public interface IRepository<T> where T : class
{
    Task<T> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<int> GetCountAsync(ListArgs args, CancellationToken cancellationToken);
    Task<T[]> GetListAsync(ListArgs args, CancellationToken cancellationToken);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken);
    Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}