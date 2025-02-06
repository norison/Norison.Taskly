namespace Norison.Taskly.Todos.Domain.Repositories;

public interface IRepository<T> where T : class
{
    Task<int> GetCountAsync(int page, int pageSize, CancellationToken cancellationToken);
    Task<T?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<T[]> GetAllAsync(CancellationToken cancellationToken);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken);
    Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}