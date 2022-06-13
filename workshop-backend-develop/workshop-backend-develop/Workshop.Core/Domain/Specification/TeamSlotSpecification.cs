using Workshop.Core.Domain.Model.TeamSlot;
using Workshop.Core.Domain.SharedKernel.Specification;

namespace Workshop.Core.Domain.Specification
{
    public static class TeamSlotSpecification
    {
        public static ISpecification<TeamSlot> GetByAuditoriumId(long id) =>
            new Specification<TeamSlot>(slot => slot.AuditoriumId == id);
        
        public static ISpecification<TeamSlot> GetById(long id) =>
            new Specification<TeamSlot>(slot => slot.Key == id);
    }
}