namespace Norison.Taskly.Todos.Domain.Repositories;

public class ListArgs
{
    public int? Page { get; set; }
    public int? PageSize { get; set; }
    public string? Filters { get; set; }
    public string? Sorts { get; set; }
}