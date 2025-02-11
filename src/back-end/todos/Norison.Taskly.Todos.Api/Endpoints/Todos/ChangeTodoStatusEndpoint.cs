using Mapster;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Norison.Taskly.Todos.Api.Constants;
using Norison.Taskly.Todos.Api.Contracts;
using Norison.Taskly.Todos.Application.UseCases.Todos.Commands.ChangeStatus;

namespace Norison.Taskly.Todos.Api.Endpoints.Todos;

public class ChangeTodoStatusEndpoint : IEndpoint
{
    public string Group => EndpointGroups.Todos;

    public void Map(RouteGroupBuilder builder)
    {
        builder.MapPut("/{id:guid}/change-status", async (
                [FromServices] ISender sender,
                [FromRoute] Guid id,
                [FromBody] ChangeTodoStatusRequest request,
                CancellationToken cancellationToken) =>
            {
                var command = request.Adapt<ChangeTodoStatusCommand>();
                await sender.Send(command, cancellationToken);
                return Results.NoContent();
            })
            .WithSummary("Change Todo Status")
            .WithDescription("Change a status of the todo")
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status204NoContent);
    }
}