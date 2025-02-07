using Mapster;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Norison.Taskly.Todos.Api.Contracts;
using Norison.Taskly.Todos.Application.DTOs;
using Norison.Taskly.Todos.Application.UseCases.Todos.Commands.Cancel;
using Norison.Taskly.Todos.Application.UseCases.Todos.Commands.Complete;
using Norison.Taskly.Todos.Application.UseCases.Todos.Commands.Create;
using Norison.Taskly.Todos.Application.UseCases.Todos.Commands.Delete;
using Norison.Taskly.Todos.Application.UseCases.Todos.Commands.Update;
using Norison.Taskly.Todos.Application.UseCases.Todos.Queries.GetById;
using Norison.Taskly.Todos.Application.UseCases.Todos.Queries.GetList;

namespace Norison.Taskly.Todos.Api.Groups;

public class TodosGroup : IGroup
{
    public void Map(IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("/todos");
        group.MapPost("/", CreateTodoAsync).ProducesValidationProblem().Produces<TodoDto>();
        group.MapGet("/", GetListAsync).ProducesValidationProblem().Produces<PagedList<TodoDto>>();
        group.MapGet("/{id:guid}", GetTodoByIdAsync).ProducesValidationProblem().Produces<TodoDto>();
        group.MapPut("/{id:guid}", UpdateTodoAsync).ProducesValidationProblem().Produces<TodoDto>();
        group.MapDelete("/{id:guid}", DeleteTodoAsync).ProducesValidationProblem().Produces<TodoDto>();;
        group.MapPut("/{id:guid}/complete", CompleteTodoAsync).ProducesValidationProblem().Produces<TodoDto>();;
        group.MapPut("/{id:guid}/cancel", CancelTodoAsync).ProducesValidationProblem().Produces<TodoDto>();;
    }

    private static async Task<IResult> CreateTodoAsync(
        [FromServices] ISender sender,
        [FromBody] CreateOrUpdateTodoRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.Adapt<CreateTodoCommand>();
        var result = await sender.Send(command, cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> GetListAsync(
        [FromServices] ISender sender,
        [FromQuery] int page,
        [FromQuery] int pageSize,
        [FromQuery] string? filters,
        [FromQuery] string? sorts,
        CancellationToken cancellationToken)
    {
        var query = new GetTodoListQuery { Page = page, PageSize = pageSize, Filters = filters, Sorts = sorts };
        var result = await sender.Send(query, cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> GetTodoByIdAsync(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetTodoByIdQuery { Id = id };
        var result = await sender.Send(query, cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> UpdateTodoAsync(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        [FromBody] CreateOrUpdateTodoRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.Adapt<UpdateTodoCommand>();
        command.Id = id;
        var result = await sender.Send(command, cancellationToken);
        return Results.Ok(result);
    }
    
    private static async Task<IResult> DeleteTodoAsync(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteTodoCommand { Id = id };
        var result = await sender.Send(command, cancellationToken);
        return Results.Ok(result);
    }
    
    private static async Task<IResult> CompleteTodoAsync(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var command = new CompleteTodoCommand { Id = id };
        var result = await sender.Send(command, cancellationToken);
        return Results.Ok(result);
    }
    
    private static async Task<IResult> CancelTodoAsync(
        [FromServices] ISender sender,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var command = new CancelTodoCommand { Id = id };
        var result = await sender.Send(command, cancellationToken);
        return Results.Ok(result);
    }
}