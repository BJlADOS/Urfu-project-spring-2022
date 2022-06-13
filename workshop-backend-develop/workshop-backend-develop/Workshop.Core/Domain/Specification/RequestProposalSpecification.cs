using Workshop.Core.Domain.Model.RequestProposal;
using Workshop.Core.Domain.SharedKernel.Specification;

namespace Workshop.Core.Domain.Specification
{
    public static class RequestProposalSpecification
    {
        public static ISpecification<RequestProposal> GetById(long id, long eventId) =>
            new Specification<RequestProposal>(u => u.EventId == eventId && u.Key == id);

        public static ISpecification<RequestProposal> GetByUserId(long id, long eventId) =>
            new Specification<RequestProposal>(u => u.EventId == eventId && u.UserId == id);

        public static ISpecification<RequestProposal> GetByTeamleadId(long id, long eventId) =>
            new Specification<RequestProposal>(u => u.EventId == eventId && u.TeamleadId == id);

        public static ISpecification<RequestProposal> GetByTeamLeadAndUserId(long TeamleadId, long userId, long eventId) =>
            new Specification<RequestProposal>(u => u.EventId == eventId && u.TeamleadId == TeamleadId && u.UserId == userId);
        public static ISpecification<RequestProposal> GetByUserIdWithStatus(long id, long eventId,RequestStatus status) =>
            new Specification<RequestProposal>(u => u.EventId == eventId && u.UserId == id && u.Status == status);
    }
}