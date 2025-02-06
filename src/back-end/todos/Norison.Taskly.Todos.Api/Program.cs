using MediatR;

using Norison.Taskly.Todos.Application;
using Norison.Taskly.Todos.Application.UseCases.Todos.Commands.Create;
using Norison.Taskly.Todos.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

app.MapOpenApi();

var todosGroup = app.MapGroup("/todos");

todosGroup.MapPost("/", async (ISender sender, CreateTodoCommand command, CancellationToken cancellationToken) =>
{
    var result = await sender.Send(command, cancellationToken);
    return Results.Ok(result);
});

app.Run();