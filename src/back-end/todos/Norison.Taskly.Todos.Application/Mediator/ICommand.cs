using MediatR;

namespace Norison.Taskly.Todos.Application.Mediator;

public interface ICommand<out TResponse> : IRequest<TResponse>;
public interface ICommand : IRequest;