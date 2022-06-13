using Workshop.Core.Domain.Model.Event;

namespace Workshop.Infrastructure.Storage.Repositories
{
    public class EventRepository : EFRepository<Event>, IEventRepository
    {
        public EventRepository(WorkshopDbContext context) : base(context)
        {
        }
    }
}
