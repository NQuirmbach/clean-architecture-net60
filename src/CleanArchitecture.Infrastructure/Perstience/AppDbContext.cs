
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Perstience;

public class AppDbContext : DbContext, IAppContext
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public AppDbContext(DbContextOptions<AppDbContext> options, IDateTimeProvider dateTimeProvider) : base(options)
    {
        _dateTimeProvider = dateTimeProvider;
    }


    public DbSet<TodoList> TodoLists { get; set; }

    public DbSet<TodoItem> TodoItems { get; set; }

    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<AppEntity>())
        {
            var entity = entry.Entity;

            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }
            if (entity.CreationDate == DateTime.MinValue)
            {
                entity.CreationDate = _dateTimeProvider.Now;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}

