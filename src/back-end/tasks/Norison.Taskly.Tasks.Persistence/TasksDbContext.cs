using Microsoft.EntityFrameworkCore;

using Norison.Taskly.Tasks.Domain.AggregateRoots;

namespace Norison.Taskly.Tasks.Persistence;

public class TasksDbContext(DbContextOptions<TasksDbContext> options) : DbContext(options)
{
    public DbSet<Todo> Todos => Set<Todo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TasksDbContext).Assembly);
    }
}