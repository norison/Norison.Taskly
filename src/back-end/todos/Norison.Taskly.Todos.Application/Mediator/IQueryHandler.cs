using MediatR;

namespace Norison.Taskly.Todos.Application.Mediator;

public interface IQueryHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IQuery<TResponse>;

public interface IQueryHandler<in TRequest> : IRequestHandler<TRequest> where TRequest : IQuery;