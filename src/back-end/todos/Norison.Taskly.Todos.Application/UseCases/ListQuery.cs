using Norison.Taskly.Todos.Application.DTOs;
using Norison.Taskly.Todos.Application.Mediator;

namespace Norison.Taskly.Todos.Application.UseCases;

public class ListQuery<T> : IQuery<PagedList<T>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string Sorts { get; set; } = null!;
    public string Filters { get; set; } = null!;
}