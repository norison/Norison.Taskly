using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Norison.Taskly.Todos.Domain.AggregateRoots;

namespace Norison.Taskly.Todos.Persistence.EntityTypeConfigurations;

public class TodoEntityTypeConfiguration : IEntityTypeConfiguration<Todo>
{
    public void Configure(EntityTypeBuilder<Todo> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(500);
        builder.Property(x => x.Status).IsRequired().HasMaxLength(20);
        builder.Property(x => x.CreatedDateTime).IsRequired();
        builder.Property(x => x.LastEditedDatetime).IsRequired();
    }
}