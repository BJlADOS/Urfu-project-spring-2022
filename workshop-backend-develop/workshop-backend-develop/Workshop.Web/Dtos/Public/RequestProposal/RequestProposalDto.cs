using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workshop.Core.Domain.Model.RequestProposal;

namespace Workshop.Web.Dtos.Public.RequestProposal
{
    public class RequestProposalDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long TeamleadId { get; set; }
        public long ProjectId { get; set; }
        public long EventId { get; set; }
        public RequestStatus Status { get; set; }

        public string RoleName { get; set; }
    }
}
