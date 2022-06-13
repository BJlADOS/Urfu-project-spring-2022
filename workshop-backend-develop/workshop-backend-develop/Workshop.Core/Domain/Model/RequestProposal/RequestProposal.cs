using Workshop.Core.Domain.SharedKernel;

namespace Workshop.Core.Domain.Model.RequestProposal
{
    public class RequestProposal : Entity<long>, IAggregateRoot, IRequestProposal
    {
        public long UserId { get; set; }
        public long TeamleadId { get; set; }
        public long ProjectId { get; set; }
        public long EventId { get; set; }
        public RequestStatus Status { get; set; }
        public string RoleName { get; set; }

        public RequestProposal()
        {
        }

        public RequestProposal(IRequestProposalDto dto, long eventId) : this(dto)
        {
            EventId = eventId;
        }

        public RequestProposal(long userId, long teamleadId, long projectId, long eventId, RequestStatus status,
            string roleName)
        {
            UserId = userId;
            TeamleadId = teamleadId;
            ProjectId = projectId;
            EventId = eventId;
            Status = status;
            RoleName = roleName;
        }

        public RequestProposal(IRequestProposalDto dto)
        {
            UserId = dto.UserId;
            TeamleadId = dto.TeamleadId;
            ProjectId = dto.ProjectId;
            Status = dto.Status;
            RoleName = dto.RoleName;
        }
    }
}