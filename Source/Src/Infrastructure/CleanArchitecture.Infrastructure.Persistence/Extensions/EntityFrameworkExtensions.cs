using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;

namespace CleanArchitecture.Infrastructure.Persistence.Extensions;

public static class EntityFrameworkExtensions
{
    /// <summary>
    /// Applies auditing information to entities that implement AuditableBaseEntity.
    /// </summary>
    /// <param name="changeTracker">The ChangeTracker instance to track entity changes.</param>
    /// <param name="authenticatedUser">The authenticated user service to get user information.</param>
    public static void ApplyAuditing(this ChangeTracker changeTracker, IAuthenticatedUserService authenticatedUser)
    {
        var userId = string.IsNullOrEmpty(authenticatedUser.UserId)
            ? Guid.Empty
            : Guid.Parse(authenticatedUser.UserId);

        var currentTime = DateTime.UtcNow;

        foreach (var entry in changeTracker.Entries())
        {
            var entityType = entry.Entity.GetType();

            if (typeof(AuditableBaseEntity).IsAssignableFrom(entityType) ||
                (entityType.BaseType?.IsGenericType ?? false) &&
                entityType.BaseType.GetGenericTypeDefinition() == typeof(AuditableBaseEntity<>))
            {
                dynamic auditableEntity = entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    auditableEntity.Created = currentTime;
                    auditableEntity.CreatedBy = userId;
                }
                else if (entry.State == EntityState.Modified)
                {
                    auditableEntity.LastModified = currentTime;
                    auditableEntity.LastModifiedBy = userId;
                }
            }
        }
    }

    /// <summary>
    /// Configures decimal properties for the given DbContext to have a specific precision and scale.
    /// </summary>
    /// <param name="context">The DbContext to apply configurations to.</param>
    /// <param name="builder">The ModelBuilder to configure entity properties.</param>
    public static void ConfigureDecimalProperties(this DbContext context, ModelBuilder builder)
    {
        foreach (var property in builder.Model.GetEntityTypes()
                     .SelectMany(t => t.GetProperties())
                     .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
        {
            property.SetColumnType("decimal(18,6)");
        }

        builder.ApplyConfigurationsFromAssembly(context.GetType().Assembly);
    }
}
