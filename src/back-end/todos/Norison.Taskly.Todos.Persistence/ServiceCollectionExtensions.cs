using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Norison.Taskly.Todos.Application.Interfaces;
using Norison.Taskly.Todos.Domain.AggregateRoots;
using Norison.Taskly.Todos.Domain.Repositories;
using Norison.Taskly.Todos.Persistence.Repositories;

namespace Norison.Taskly.Todos.Persistence;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TodosDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("TodosDb"));
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IRepository<Todo>, GenericRepository<Todo>>();

        return services;
    }
}