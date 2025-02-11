using System.Reflection;

using Microsoft.EntityFrameworkCore;

using Norison.Taskly.Todos.Api.Endpoints;
using Norison.Taskly.Todos.Persistence;

using Scalar.AspNetCore;

namespace Norison.Taskly.Todos.Api.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseApi(this WebApplication app)
    {
        app.MapOpenApi();
        app.MapScalarApiReference();
        app.MapGroups();

        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TodosDbContext>();
        dbContext.Database.Migrate();

        return app;
    }

    private static void MapGroups(this WebApplication app)
    {
        var groups = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(x => x is { IsAbstract: false, IsClass: true } && typeof(IEndpoint).IsAssignableFrom(x))
            .Select(Activator.CreateInstance)
            .Cast<IEndpoint>()
            .GroupBy(x => x.Group);

        foreach (var group in groups)
        {
            var newGroup = app.MapGroup(group.Key);
            group.ToList().ForEach(x => x.Map(newGroup));
        }
    }
}