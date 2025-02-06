namespace Norison.Taskly.Todos.Domain.Shared.Exceptions;

public class TasklyException(string code, params string[] parameters) : Exception(code)
{
    public string[] Parameters { get; } = parameters;
}