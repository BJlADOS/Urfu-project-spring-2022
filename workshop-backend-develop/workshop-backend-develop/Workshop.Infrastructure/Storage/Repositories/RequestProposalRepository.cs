using Workshop.Core.Domain.Model.RequestProposal;

namespace Workshop.Infrastructure.Storage.Repositories
{
    public class RequestProposalRepository : EFRepository<RequestProposal>, IRequestProposalRepository
    {
        public RequestProposalRepository(WorkshopDbContext context) : base(context)
        {
        }
    }
}