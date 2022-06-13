using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;

namespace Workshop.Web.Features.Public.ProjectProposal
{
    public class DeleteProjectProposalCommand : IRequest
    {
        public long Id { get; set; }
    }

    public class
        DeleteProjectProposalCommandHandler : AsyncRequestHandler<DeleteProjectProposalCommand>
    {
        private readonly IUserProfileProvider _profileProvider;
        private readonly ProjectProposalRepository _projectProposalRepository;

        public DeleteProjectProposalCommandHandler(IUserProfileProvider profileProvider,
                                                   ProjectProposalRepository
                                                       projectProposalRepository)
        {
            _profileProvider = profileProvider;
            _projectProposalRepository = projectProposalRepository;
        }

        protected override async Task Handle(DeleteProjectProposalCommand request,
                                             CancellationToken cancellationToken)
        {
            var user = _profileProvider.GetProfile().User;
            var proposal = await _projectProposalRepository.SingleOrDefaultAsync(
             ProjectProposalSpecification.GetById(request.Id, user.EventId), cancellationToken);
            if (proposal is null ||
                user.UserType != UserType.Admin && user.Key != proposal.AuthorId)
            {
                return;
            }

            await _projectProposalRepository.RemoveAsync(proposal);
            await _projectProposalRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}