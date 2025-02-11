using MediatR;

using Microsoft.AspNetCore.Mvc;

using Norison.Taskly.Todos.Api.Constants;
using Norison.Taskly.Todos.Application.DTOs;
using Norison.Taskly.Todos.Application.UseCases.Todos.Queries.GetById;

namespace Norison.Taskly.Todos.Api.Endpoints.Todos;

public class GetTodoEndpoint : IEndpoint
{
    public string Group => EndpointGroups.Todos;

    public void Map(RouteGroupBuilder builder)
    {
        builder.MapGet("/{id:guid}", async (
                [FromServices] ISender sender,
                [FromRoute] Guid id,
                CancellationToken cancellationToken) =>
            {
                var query = new GetTodoByIdQuery { Id = id };
                var result = await sender.Send(query, cancellationToken);
                return Results.Ok(result);
            })
            .WithSummary("Get Todo")
            .WithDescription("Get a todo by id")
            .ProducesValidationProblem()
            .Produces<TodoDto>();
    }
}