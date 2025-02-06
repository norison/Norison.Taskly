using Norison.Taskly.Todos.Application;
using Norison.Taskly.Todos.Persistence;

namespace Norison.Taskly.Todos.Api;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOpenApi();
        services.AddPersistence(configuration);
        services.AddApplication();
        return services;
    }
}