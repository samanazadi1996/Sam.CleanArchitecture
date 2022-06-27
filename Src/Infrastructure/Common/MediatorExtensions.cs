using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MediatR
{
    public static class MediatorExtensions
    {
        public static async Task DispatchDomainEvents(this IMediator mediator, DbContext context)
        {
            var entities = context.ChangeTracker
                .Entries<BaseEntity>()
                .Where(e => e.Entity.DomainEvents.Any())
                .Select(e => e.Entity);

            var domainEvents = entities
                .SelectMany(e => e.DomainEvents)
                .ToList();

            entities.ToList().ForEach(e => e.ClearDomainEvents());

            var deletedEntity = context.ChangeTracker.Entries<BaseEntity>().Where(p => p.State == EntityState.Deleted).Select(p => p.Entity);
            foreach (var item in deletedEntity)
            {
                var makeme = typeof(BaseEventDeleted<>).MakeGenericType(item.GetType());
                var type = Activator.CreateInstance(makeme);
                MethodInfo method = type.GetType().GetMethod("SetId");
                method.Invoke(type, new List<object>() { item.Id }.ToArray());

                await mediator.Publish(type);
            }


            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}