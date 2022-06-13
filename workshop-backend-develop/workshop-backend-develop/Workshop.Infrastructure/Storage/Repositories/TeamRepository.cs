using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Workshop.Core.Domain.Model.Team;
using Workshop.Core.Domain.SharedKernel.Specification;

namespace Workshop.Infrastructure.Storage.Repositories
{
    public class TeamRepository : EFRepository<Team>, ITeamRepository
    {
        public TeamRepository(WorkshopDbContext context) : base(context)
        {
        }

        protected override IQueryable<Team> Items => base.Items
            .Include(x => x.Users)
            .ThenInclude(x => x.Competencies)
            .ThenInclude(x => x.Competency)
            .Include(x => x.Users)
            .ThenInclude(x => x.Role)
            .Include(x => x.Expert)
            .Include(x => x.Auditorium)
            .Include(x => x.Project)
            .ThenInclude(x => x.Roles)
            .Include(x => x.TeamReviews)
            .Include(x => x.TeamCompetencyReviews);

        public Task<Team[]> ShallowListAsync(ISpecification<Team> specification, CancellationToken cancellationToken)
        {
            return base.Items
                .Where(specification.IsSatisfiedBy())
                .Include(t => t.Users)
                .Include(t => t.Project)
                .Include(t => t.TeamSlot)
                .ToArrayAsync(cancellationToken);
        }

        public Task<Team[]> TeamsWithReviewListAsync(ISpecification<Team> specification, CancellationToken cancellationToken)
        {
            return base.Items
                .Where(specification.IsSatisfiedBy())
                .Include(t => t.Users)
                .Include(t => t.Project)
                .Include(t => t.TeamSlot)
                .Include(x => x.TeamReviews)
                .ThenInclude(x => x.Expert)
                .Include(x => x.TeamCompetencyReviews)
                .ToArrayAsync(cancellationToken);
        }

        public Task<Team[]> ListWithSlotsAsync(ISpecification<Team> specification, CancellationToken cancellationToken)
        {
            return base.Items
                .Where(specification.IsSatisfiedBy())
                .Include(t => t.Users)
                .Include(t => t.Project)
                .Include(t => t.TeamSlot)
                .ThenInclude(t => t.Auditorium)
                .ToArrayAsync(cancellationToken);
        }
    }
}