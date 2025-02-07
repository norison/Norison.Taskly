using MediatR;

using Microsoft.Extensions.Logging;

namespace Norison.Taskly.Todos.Application.Mediator.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        try
        {
            logger.LogInformation("Handling {RequestName}", requestName);
            var response = next();
            logger.LogInformation("Handled {RequestName}", requestName);
            return response;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error handling {RequestName}", requestName);
            throw;
        }
    }
}