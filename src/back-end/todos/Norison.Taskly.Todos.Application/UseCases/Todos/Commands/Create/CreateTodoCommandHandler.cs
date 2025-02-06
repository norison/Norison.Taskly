using Norison.Taskly.Todos.Application.Mediator;

namespace Norison.Taskly.Todos.Application.UseCases.Todos.Commands.Create;

public class CreateTodoCommandHandler : ICommandHandler<CreateTodoCommand, CreateTodoCommandResult>
{
    public Task<CreateTodoCommandResult> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}