using N5NowApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using N5NowApi.Domain.Common;
using MediatR;
using N5NowApi.Domain.Primitives;

namespace Infrastructure.Persistance;

public class ApplicationDbContext : DbContext
{
    private readonly IPublisher _publisher;
    public ApplicationDbContext(DbContextOptions options, IPublisher publisher) : base(options)
    {
        _publisher = publisher;
    }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<PermissionType> PermissionsType { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var domainEntities = ChangeTracker.Entries<AggregateRoot>();

        foreach(var domainEntity in domainEntities)
        {
            var domainEvents = domainEntity.Entity.Events;

            foreach(var @event in domainEvents)
            {
                _publisher.Publish(@event, cancellationToken);
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
