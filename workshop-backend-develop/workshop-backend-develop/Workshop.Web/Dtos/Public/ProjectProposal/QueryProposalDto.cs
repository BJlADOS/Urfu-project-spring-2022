using Workshop.Core.Domain.Model.ProjectProposal;

namespace Workshop.Web.Dtos.Public.ProjectProposal
{
    public class QueryProposalDto
    {
        public long? AuthorId { get; set; }
        public ProposalStatus? Status { get; set; }
    }
}