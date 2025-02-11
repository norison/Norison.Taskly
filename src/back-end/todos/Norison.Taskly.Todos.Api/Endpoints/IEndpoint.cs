namespace Norison.Taskly.Todos.Api.Endpoints;

public interface IEndpoint
{
    string Group { get; }
    void Map(RouteGroupBuilder builder);
}