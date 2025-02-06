namespace Norison.Taskly.Todos.Application.Services.GuidGenerator;

public class GuidGenerator : IGuidGenerator
{
    public Guid Create()
    {
        return Guid.CreateVersion7();
    }
}