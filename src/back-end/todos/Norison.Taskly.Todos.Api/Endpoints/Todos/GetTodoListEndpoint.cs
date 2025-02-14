using MediatR;

using Microsoft.AspNetCore.Mvc;

using Norison.Taskly.Todos.Api.Constants;
using Norison.Taskly.Todos.Application.DTOs;
using Norison.Taskly.Todos.Application.UseCases.Todos.Queries.GetList;

namespace Norison.Taskly.Todos.Api.Endpoints.Todos;

public class GetTodoListEndpoint : IEndpoint
{
    public string Group => EndpointGroups.Todos;

    public void Map(RouteGroupBuilder builder)
    {
        builder.MapGet("/", async (
                [FromServices] ISender sender,
                [FromHeader(Name = "X-User-Id")] Guid userId,
                [FromQuery] int page,
                [FromQuery] int pageSize,
                [FromQuery] string? filters,
                [FromQuery] string? sorts,
                CancellationToken cancellationToken) =>
            {
                var query = new GetTodoListQuery { Page = page, PageSize = pageSize, Filters = filters, Sorts = sorts };
                var result = await sender.Send(query, cancellationToken);
                return Results.Ok(result);
            })
            .WithSummary("Get Todo List")
            .WithDescription("Get a list of todos")
            .ProducesValidationProblem()
            .Produces<PagedList<TodoDto>>();
    }
}