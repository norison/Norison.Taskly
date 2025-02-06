using MediatR;

using Microsoft.AspNetCore.Mvc;

using Norison.Taskly.Todos.Application.UseCases.Todos.Commands.Create;
using Norison.Taskly.Todos.Application.UseCases.Todos.Queries.GetList;

namespace Norison.Taskly.Todos.Api.Groups;

public class TodosGroup : IGroup
{
    public void Map(IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("/todos");
        group.MapPost("/", CreateTodoAsync);
        group.MapGet("/", GetListAsync);
    }

    private static async Task<IResult> CreateTodoAsync(
        [FromServices] ISender sender,
        [FromBody] CreateTodoCommand command,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> GetListAsync(
        [FromServices] ISender sender,
        [FromQuery] int page,
        [FromQuery] int pageSize,
        [FromQuery] string filters,
        [FromQuery] string sorts,
        CancellationToken cancellationToken)
    {
        var query = new GetTodoListQuery { Page = page, PageSize = pageSize, Filters = filters, Sorts = sorts };
        var result = await sender.Send(query, cancellationToken);
        return Results.Ok(result);
    }
}