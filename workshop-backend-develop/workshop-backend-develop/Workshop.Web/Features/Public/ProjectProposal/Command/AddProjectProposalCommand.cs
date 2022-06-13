using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;
using Workshop.Web.Dtos.Public.ProjectProposal;

namespace Workshop.Web.Features.Public.ProjectProposal
{
    public class AddProjectProposalCommand : IRequest
    {
        public AddProjectProposalDto AddProjectProposalDto { get; set; }
    }

    public class AddProjectProposalCommandHandler : AsyncRequestHandler<AddProjectProposalCommand>
    {
        private readonly IUserProfileProvider _profileProvider;
        private readonly ProjectProposalRepository _projectProposalRepository;

        public AddProjectProposalCommandHandler(IUserProfileProvider profileProvider,
                                                ProjectProposalRepository projectProposalRepository)
        {
            _profileProvider = profileProvider;
            _projectProposalRepository = projectProposalRepository;
        }

        protected override async Task Handle(AddProjectProposalCommand request,
                                             CancellationToken cancellationToken)
        {
            var user = _profileProvider.GetProfile().User;
            var proposals = await _projectProposalRepository.LongCountAsync(ProjectProposalSpecification.GetByAuthorId(user.EventId, user.Key), cancellationToken);
            if (proposals > 0)
            {
                return;
            }
            var proposal =
                new Core.Domain.Model.ProjectProposal.ProjectProposal(user,
                 request.AddProjectProposalDto);
            await _projectProposalRepository.AddAsync(proposal, cancellationToken);
            await _projectProposalRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}