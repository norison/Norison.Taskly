using Mapster;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Norison.Taskly.Todos.Api.Constants;
using Norison.Taskly.Todos.Api.Contracts;
using Norison.Taskly.Todos.Application.UseCases.Todos.Commands.Create;

namespace Norison.Taskly.Todos.Api.Endpoints.Todos;

public class CreateTodoEndpoint : IEndpoint
{
    public string Group => EndpointGroups.Todos;

    public void Map(RouteGroupBuilder builder)
    {
        builder.MapPost("/", async (
                [FromServices] ISender sender,
                [FromBody] CreateOrUpdateTodoRequest request,
                CancellationToken cancellationToken) =>
            {
                var command = request.Adapt<CreateTodoCommand>();
                var result = await sender.Send(command, cancellationToken);
                return Results.Created($"/todos/{result}", new CreateTodoResponse { Id = result });
            })
            .WithSummary("Create Todo")
            .WithDescription("Creates a todo")
            .ProducesValidationProblem()
            .Produces<CreateTodoResponse>(StatusCodes.Status201Created);
    }
}