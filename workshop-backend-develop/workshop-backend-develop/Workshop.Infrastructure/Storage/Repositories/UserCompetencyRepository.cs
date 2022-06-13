using System.Linq;
using Workshop.Core.Domain.Model.UserCompetency;

namespace Workshop.Infrastructure.Storage.Repositories
{
    public class UserCompetencyRepository : EFRepository<UserCompetency>, IUserCompetencyRepository
    {
        public UserCompetencyRepository(WorkshopDbContext context) : base(context)
        {
        }

        protected IQueryable<UserCompetency> GetCompetencies()
        {
            return base.Items;
        }
    }
}