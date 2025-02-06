using FluentValidation;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using Norison.Taskly.Todos.Application.Mediator.Behaviors;
using Norison.Taskly.Todos.Application.Services.DateNow;
using Norison.Taskly.Todos.Application.Services.GuidGenerator;

namespace Norison.Taskly.Todos.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
            config.Lifetime = ServiceLifetime.Scoped;

            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>), ServiceLifetime.Scoped);
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>), ServiceLifetime.Scoped);
        });

        services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);

        services.AddScoped<IGuidGenerator, GuidGenerator>();
        services.AddScoped<IDateNowService, DateNowService>();

        return services;
    }
}