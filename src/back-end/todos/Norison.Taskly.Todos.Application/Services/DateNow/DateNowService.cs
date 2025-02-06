namespace Norison.Taskly.Todos.Application.Services.DateNow;

public class DateNowService : IDateNowService
{
    public DateTime Now => DateTime.UtcNow;
}