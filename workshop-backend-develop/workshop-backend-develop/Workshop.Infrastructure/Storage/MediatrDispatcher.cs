using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop.Core.Domain.SharedKernel;
using MediatR;

namespace Workshop.Infrastructure.Storage
{
    public static class MediatrDispatcher
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, WorkshopDbContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) => {
                    await mediator.Publish(domainEvent);
                }).ToArray();

            await Task.WhenAll(tasks);
        }
    }
}
