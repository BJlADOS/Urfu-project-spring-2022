using Workshop.Core.Domain.Model.UserCompetency;
using Workshop.Core.Domain.SharedKernel.Specification;

namespace Workshop.Core.Domain.Specification
{
    public class UserCompetencySpecification
    {
        public static ISpecification<UserCompetency> GetByUser(long id) =>
            new Specification<UserCompetency>(x => x.UserId == id);
    }
}