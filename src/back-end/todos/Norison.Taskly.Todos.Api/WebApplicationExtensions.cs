using System.Reflection;

using Norison.Taskly.Todos.Api.Groups;

namespace Norison.Taskly.Todos.Api;

public static class WebApplicationExtensions
{
    public static WebApplication UseApi(this WebApplication app)
    {
        app.MapOpenApi();
        app.MapGroups();
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