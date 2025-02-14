using FluentValidation;

using Norison.Taskly.Todos.Domain.Exceptions;

namespace Norison.Taskly.Todos.Api.Endpoints.Filters;

public class ErrorEndpointFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        try
        {
            return await next(context);
        }
        catch (Exception exception)
        {
            return exception switch
            {
                NotFoundDomainException notFoundDomainException => Results.Problem(notFoundDomainException.Message,
                    statusCode: StatusCodes.Status404NotFound),
                ValidationDomainException validationDomainException =>
                    Results.Problem(validationDomainException.Message, statusCode: StatusCodes.Status400BadRequest),
                ValidationException validationException => Results.ValidationProblem(
                    validationException.Errors.ToDictionary(error => error.ErrorCode,
                        error => new[] { error.ErrorMessage })),
                _ => Results.Problem(exception.Message, statusCode: StatusCodes.Status500InternalServerError)
            };
        }
    }
}