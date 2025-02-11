using Mapster;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Norison.Taskly.Todos.Api.Constants;
using Norison.Taskly.Todos.Api.Contracts;
using Norison.Taskly.Todos.Application.UseCases.Todos.Commands.Update;

namespace Norison.Taskly.Todos.Api.Endpoints.Todos;

public class UpdateTodoEndpoint : IEndpoint
{
    public string Group => EndpointGroups.Todos;

    public void Map(RouteGroupBuilder builder)
    {
        builder.MapPut("/{id:guid}", async (
                [FromServices] ISender sender,
                [FromRoute] Guid id,
                [FromBody] CreateOrUpdateTodoRequest request,
                CancellationToken cancellationToken) =>
            {
                var command = request.Adapt<UpdateTodoCommand>();
                command.Id = id;
                await sender.Send(command, cancellationToken);
                return Results.NoContent();
            })
            .WithSummary("Update Todo")
            .WithDescription("Updates a todo")
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status204NoContent);
    }
}