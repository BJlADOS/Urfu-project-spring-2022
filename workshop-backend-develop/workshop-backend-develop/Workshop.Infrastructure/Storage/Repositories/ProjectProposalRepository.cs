using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Workshop.Core.Domain.Model.ProjectProposal;
using Workshop.Core.Domain.SharedKernel.Specification;

namespace Workshop.Infrastructure.Storage.Repositories
{
    public class ProjectProposalRepository : EFRepository<ProjectProposal>, IProjectProposalRepository
    {
        protected override IQueryable<ProjectProposal> Items => base.Items
                .Include(proposal => proposal.Author);
        public ProjectProposalRepository(WorkshopDbContext context) : base(context)
        {
        }
    }
}