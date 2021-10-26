using CleanArchitecture.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Perstience.Configs;

internal class TodoListConfiguration : IEntityTypeConfiguration<TodoList>
{
    public void Configure(EntityTypeBuilder<TodoList> builder)
    {
        builder.ToTable("TodoLists");

        builder.Property(m => m.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasMany(m => m.Items)
            .WithOne(m => m.List)
            .HasForeignKey(m => m.ListId);
    }
}

