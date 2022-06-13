using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Domain.SharedKernel.Specification;

namespace Workshop.Infrastructure.Storage.Repositories
{
    public class UserRepository : EFRepository<User>, IUserRepository
    {
        public UserRepository(WorkshopDbContext context) : base(context)
        {
        }

        protected override IQueryable<User> Items => base.Items
            .Include(user => user.Team)
            .ThenInclude(team => team.TeamSlot)
            .Include(user => user.Role)
            .Include(user => user.Auditoriums)
            .Include(user => user.Competencies)
            .ThenInclude(userCompetency => userCompetency.Competency);

        public Task<User[]> ShallowListAsync(ISpecification<User> specification, CancellationToken cancellationToken)
        {
            return base.Items
                .Where(specification.IsSatisfiedBy())
                .ToArrayAsync(cancellationToken);
        }

        public Task<User[]> ShallowListAsyncWithTeams(ISpecification<User> specification,
            CancellationToken cancellationToken)
        {
            return base.Items
                .Include(user => user.Role)
                .Include(user => user.Team)
                .ThenInclude(team => team.Project)
                .Include(user => user.Team.Project.KeyTechnology)
                .Include(user => user.Team.Project.LifeScenario)
                .Where(specification.IsSatisfiedBy())
                .ToArrayAsync(cancellationToken);
        }

        public Task<User[]> ShallowListAsyncWithCompetencies(ISpecification<User> specification,
            CancellationToken cancellationToken)
        {
            return base.Items
                .Where(specification.IsSatisfiedBy())
                .Include(user => user.Competencies)
                .ThenInclude(userCompetency => userCompetency.Competency)
                .ToArrayAsync(cancellationToken);
        }

        public Task<User[]> ShallowListAsyncWithRoles(ISpecification<User> specification,
            CancellationToken cancellationToken)
        {
            return base.Items
                .Include(user => user.Role)
                .Where(specification.IsSatisfiedBy())
                .ToArrayAsync(cancellationToken);
        }

        public Task<User> DetailedUserInfo(ISpecification<User> specification, CancellationToken cancellationToken)
        {
            return base.Items
                .Where(specification.IsSatisfiedBy())
                .Include(user => user.Team)
                .ThenInclude(team => team.Project)
                .Include(user => user.Competencies)
                .ThenInclude(userCompetency => userCompetency.Competency)
                .Include(user => user.Role)
                .SingleAsync(cancellationToken);
        }

        public Task<List<User>> GetUsers(CancellationToken cancellationToken)
        {
            return base.Items
                .Include(user => user.Competencies)
                .ThenInclude(userCompetency => userCompetency.Competency)
                .ToListAsync(cancellationToken);
        }
    }
}