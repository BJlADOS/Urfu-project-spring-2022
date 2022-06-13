using MediatR;
using Workshop.Core.Domain.Model.RequestProposal;

namespace Workshop.Web.Dtos.Public.RequestProposal
{
    public class AddRequestProposalDto : IRequest, IRequestProposalDto
    {
        public long UserId { get; set; }
        public long TeamleadId { get; set; }
        public long ProjectId { get; set; }

        public string RoleName { get; set; }
        public RequestStatus Status { get; set; }
    }
}