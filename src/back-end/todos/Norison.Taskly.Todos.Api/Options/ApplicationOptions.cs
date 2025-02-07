using System.ComponentModel.DataAnnotations;

namespace Norison.Taskly.Todos.Api.Options;

public class ApplicationOptions
{
    [Required]
    public string Name { get; set; } = string.Empty;
}