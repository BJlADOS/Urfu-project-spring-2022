using Workshop.Core.Domain.Model.RequestProposal;

namespace Workshop.Web.Dtos.Public.RequestProposal
{
    public class UpdateStatusRequestProposalDto
    {
        public RequestStatus Status { get; set; }
    }
}