using MediatR;

namespace Norison.Taskly.Todos.Application.Mediator;

public interface IQuery<out TResponse> : IRequest<TResponse>;
public interface IQuery : IRequest;