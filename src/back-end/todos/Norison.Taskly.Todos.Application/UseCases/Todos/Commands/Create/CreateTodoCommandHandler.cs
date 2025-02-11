using Norison.Taskly.Todos.Application.Mediator;
using Norison.Taskly.Todos.Application.Services.DateNow;
using Norison.Taskly.Todos.Application.Services.GuidGenerator;
using Norison.Taskly.Todos.Domain.AggregateRoots;
using Norison.Taskly.Todos.Domain.Repositories;

namespace Norison.Taskly.Todos.Application.UseCases.Todos.Commands.Create;

public class CreateTodoCommandHandler(
    IRepository<Todo> todosRepository,
    IGuidGenerator guidGenerator,
    IDateNowService dateNowService) : ICommandHandler<CreateTodoCommand, Guid>
{
    public async Task<Guid> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = new Todo(guidGenerator.Create(), request.Title, request.Description, dateNowService.Now);
        await todosRepository.AddAsync(todo, cancellationToken);
        return todo.Id;
    }
}