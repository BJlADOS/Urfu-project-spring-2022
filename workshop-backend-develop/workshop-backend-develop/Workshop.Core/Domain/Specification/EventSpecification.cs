using Workshop.Core.Domain.Model.Event;
using Workshop.Core.Domain.SharedKernel.Specification;

namespace Workshop.Core.Domain.Specification
{
    public static class EventSpecification
    {
        public static ISpecification<Event> GetById(long id) => new Specification<Event>(u => u.Key == id);

        public static ISpecification<Event> GetAllActive => new Specification<Event>(u => u.IsActive);
    }
}
