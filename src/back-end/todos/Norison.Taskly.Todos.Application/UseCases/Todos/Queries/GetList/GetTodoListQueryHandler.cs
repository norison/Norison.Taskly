using Mapster;

using Norison.Taskly.Todos.Application.DTOs;
using Norison.Taskly.Todos.Application.Mediator;
using Norison.Taskly.Todos.Domain.AggregateRoots;
using Norison.Taskly.Todos.Domain.Repositories;

namespace Norison.Taskly.Todos.Application.UseCases.Todos.Queries.GetList;

public class GetTodoListQueryHandler(IRepository<Todo> todosRepository)
    : IQueryHandler<GetTodoListQuery, PagedList<TodoDto>>
{
    public async Task<PagedList<TodoDto>> Handle(GetTodoListQuery request, CancellationToken cancellationToken)
    {
        var args = request.Adapt<ListArgs>();
        var count = await todosRepository.GetCountAsync(args, cancellationToken);
        var todos = await todosRepository.GetListAsync(args, cancellationToken);
        return new PagedList<TodoDto> { TotalCount = count, Data = todos.Adapt<TodoDto[]>() };
    }
}