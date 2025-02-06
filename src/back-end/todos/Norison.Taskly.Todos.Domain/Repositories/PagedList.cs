namespace Norison.Taskly.Todos.Domain.Repositories;

public class PagedList<T>
{
    public int TotalCount { get; set; }
    public T[] Data { get; set; } = [];
}