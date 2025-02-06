namespace Norison.Taskly.Todos.Application.DTOs;

public class PagedList<T>
{
    public int TotalCount { get; set; }
    public T[] Data { get; set; } = [];
}