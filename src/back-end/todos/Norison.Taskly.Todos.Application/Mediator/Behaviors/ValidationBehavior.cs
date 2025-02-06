using FluentValidation;

using MediatR;

namespace Norison.Taskly.Todos.Application.Mediator.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var tasks = validators.Select(x => x.ValidateAsync(request, cancellationToken));

        var results = await Task.WhenAll(tasks);

        var failures = results
            .Where(x => !x.IsValid)
            .SelectMany(x => x.Errors)
            .ToArray();

        if (failures.Length == 0)
        {
            return await next();
        }

        throw new ValidationException(failures);
    }
}