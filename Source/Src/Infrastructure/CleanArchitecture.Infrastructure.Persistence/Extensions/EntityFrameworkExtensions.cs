using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CleanArchitecture.Infrastructure.Persistence.Extensions;

public static class EntityFrameworkExtensions
{
    public static void ApplyAuditing(this ChangeTracker changeTracker, IAuthenticatedUserService authenticatedUser, IAuditLogService auditLog)
    {
        var userId = string.IsNullOrEmpty(authenticatedUser.UserId)
            ? Guid.Empty
            : Guid.Parse(authenticatedUser.UserId);

        var currentTime = DateTime.UtcNow;

        foreach (var entry in changeTracker.Entries())
        {
            var entityType = entry.Entity.GetType();
            if (entry.State == EntityState.Added)
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType) ||
                    (entityType.BaseType?.IsGenericType ?? false) &&
                    entityType.BaseType.GetGenericTypeDefinition() == typeof(BaseEntity<>))
                {
                    dynamic auditableEntity = entry.Entity;


                    auditableEntity.Created = currentTime;
                    auditableEntity.CreatedBy = userId;
                }
            }
            else if (entry.State == EntityState.Modified)
            {

                if (TryGetModifiedPropertiesWithOldValues(entry, out var oldValue, out var newVale))
                {
                    var primaryKey = entry.Properties.FirstOrDefault(p => p.Metadata.IsPrimaryKey());
                    var id = primaryKey?.CurrentValue?.ToString() ?? "Unknown ID";
                    auditLog.Append(id, entityType, oldValue, newVale, authenticatedUser.UserId);
                }
            }
        }

        auditLog.SaveLogsAsync();
    }

    private static bool TryGetModifiedPropertiesWithOldValues(EntityEntry entry, out Dictionary<string, object> oldValue, out Dictionary<string, object> newValue)
    {
        var hasChange = false;
        var originalValues = entry.OriginalValues;
        var currentValues = entry.CurrentValues;

        oldValue = [];
        newValue = [];

        foreach (var property in originalValues.Properties)
        {
            var originalValue = originalValues[property];
            var currentValue = currentValues[property];

            if (!Equals(originalValue, currentValue))  // Only capture changed properties
            {
                oldValue.Add(property.Name, originalValue);  // Store the original value for changed properties
                newValue.Add(property.Name, currentValue);  // Store the original value for changed properties
                hasChange = true;
            }
        }

        return hasChange;
    }

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
