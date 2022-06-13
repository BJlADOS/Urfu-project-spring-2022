using Workshop.Core.Domain.Model.ProjectProposal;
using Workshop.Core.Domain.SharedKernel.Specification;

namespace Workshop.Core.Domain.Specification
{
    public static class ProjectProposalSpecification
    {
        public static ISpecification<ProjectProposal> GetById(long id, long eventId) =>
            new Specification<ProjectProposal>(proposal =>
                                                   proposal.EventId == eventId && proposal.Key == id);

        public static ISpecification<ProjectProposal> GetAll(long eventId) =>
            new Specification<ProjectProposal>(proposal => proposal.EventId == eventId);
        
        public static ISpecification<ProjectProposal> GetByQuery(long eventId, long? authorId, ProposalStatus? status) =>
            new Specification<ProjectProposal>(proposal => proposal.EventId == eventId
                                              && (!authorId.HasValue || proposal.AuthorId == authorId.Value)
                                              && (!status.HasValue || proposal.Status == status.Value));
        public static ISpecification<ProjectProposal> GetPendingProposals(long eventId) =>
            new Specification<ProjectProposal>(proposal => proposal.EventId == eventId 
                                               && proposal.Status == ProposalStatus.Pending);
        public static ISpecification<ProjectProposal> GetByAuthorId(long eventId, long? authorId) =>
            new Specification<ProjectProposal>(proposal => proposal.EventId == eventId 
                                              && (!authorId.HasValue || proposal.AuthorId == authorId.Value));

    }
}