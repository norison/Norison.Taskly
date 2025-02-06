using Microsoft.EntityFrameworkCore;

using Norison.Taskly.Todos.Domain.AggregateRoots;

namespace Norison.Taskly.Todos.Persistence;

public class TasksDbContext(DbContextOptions<TasksDbContext> options) : DbContext(options)
{
    public DbSet<Todo> Todos => Set<Todo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TasksDbContext).Assembly);
    }
}