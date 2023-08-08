using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Sam.CleanArchitecture.Application.Interfaces;
using Sam.CleanArchitecture.Domain.Common;
using Sam.CleanArchitecture.Domain.OutBoxEventItems.Entities;

namespace Sam.CleanArchitecture.Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IAuthenticatedUserService authenticatedUser;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IAuthenticatedUserService authenticatedUser) : base(options)
        {
            this.authenticatedUser = authenticatedUser;
        }

        public DbSet<OutBoxEventItem> OutBoxEventItems { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            AddDomainEvents();
            if (authenticatedUser.UserId is not null)
            {
                var userId = Guid.Parse(authenticatedUser.UserId);
                foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.Entity.Created = DateTime.Now;
                            entry.Entity.CreatedBy = userId;
                            break;
                        case EntityState.Modified:
                            entry.Entity.LastModified = DateTime.Now;
                            entry.Entity.LastModifiedBy = userId;
                            break;
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //All Decimals will have 18,6 Range
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,6)");
            }
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            base.OnModelCreating(builder);
        }
        private void AddDomainEvents()
        {
            #region AddDomainEvents

            var entities = ChangeTracker
                .Entries<BaseEntity>()
                .Where(e => e.Entity.DomainEvents.Any())
                .Select(e => e.Entity);

            var domainEvents = entities
                .SelectMany(e => e.DomainEvents)
                .ToList();

            entities.ToList().ForEach(e => e.ClearDomainEvents());

            foreach (var item in domainEvents)
                Add(item.GetType(), item);

            #endregion

            #region AddDeletedEvents

            var deletedEntity = ChangeTracker.Entries<BaseEntity>().Where(p => p.State == EntityState.Deleted).Select(p => p.Entity);
            foreach (var item in deletedEntity)
                Add(typeof(BaseEventDeleted<>).MakeGenericType(item.GetType()), new { item.Id });

            #endregion

            void Add(Type @type, object item)
            {
                OutBoxEventItems.Add(new OutBoxEventItem()
                {
                    EventName = @type.Name,
                    EventTypeName = @type.FullName,
                    EventPayload = JsonSerializer.Serialize(item)
                });
            }
        }
    }
}
