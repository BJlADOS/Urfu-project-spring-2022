using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Workshop.Core.Domain.Model.Project;
using Workshop.Core.Domain.Model.Team;
using Workshop.Core.Domain.SharedKernel.Specification;

namespace Workshop.Infrastructure.Storage.Repositories
{
    public class ProjectRepository : EFRepository<Project>, IProjectRepository
    {
        public ProjectRepository(WorkshopDbContext context) : base(context)
        {
        }

        protected override IQueryable<Project> Items => base.Items
            .Include(x => x.KeyTechnology)
            .Include(x => x.LifeScenario)
            .Include(x => x.Roles)
            .Include(x => x.Competencies)
            .ThenInclude(x => x.Competency);

        public Task<Project[]> ShallowListAsync(ISpecification<Project> specification, CancellationToken cancellationToken)
        {
            return base.Items
                .Where(specification.IsSatisfiedBy())
                .ToArrayAsync(cancellationToken);
        }
    }
}