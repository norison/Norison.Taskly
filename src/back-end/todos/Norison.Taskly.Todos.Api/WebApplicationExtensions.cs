using System.Reflection;

using Microsoft.EntityFrameworkCore;

using Norison.Taskly.Todos.Api.Groups;
using Norison.Taskly.Todos.Persistence;

using Scalar.AspNetCore;

namespace Norison.Taskly.Todos.Api;

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
        Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(x => x is { IsAbstract: false, IsClass: true } && typeof(IGroup).IsAssignableFrom(x))
            .Select(Activator.CreateInstance)
            .Cast<IGroup>()
            .ToList()
            .ForEach(x => x.Map(app));
    }
}