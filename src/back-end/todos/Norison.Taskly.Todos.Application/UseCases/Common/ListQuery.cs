using Norison.Taskly.Todos.Application.DTOs;
using Norison.Taskly.Todos.Application.Mediator;

namespace Norison.Taskly.Todos.Application.UseCases.Common;

public class ListQuery<T> : IQuery<PagedList<T>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string? Sorts { get; set; }
    public string? Filters { get; set; }
}