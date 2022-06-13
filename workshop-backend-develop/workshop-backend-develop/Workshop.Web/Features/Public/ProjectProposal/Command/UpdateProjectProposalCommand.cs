using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;
using Workshop.Web.Dtos.Public.ProjectProposal;
using Workshop.Web.Exceptions;

namespace Workshop.Web.Features.Public.ProjectProposal
{
    public class UpdateProjectProposalCommand : IRequest
    {
        public long Id { get; set; }
        public AddProjectProposalDto UpdateProjectProposalDto { get; set; }
    }

    public class UpdateProjectProposalCommandHandler : AsyncRequestHandler<UpdateProjectProposalCommand>
    {
        private readonly IUserProfileProvider _profileProvider;
        private readonly ProjectProposalRepository _projectProposalRepository;

        public UpdateProjectProposalCommandHandler(ProjectProposalRepository projectProposalRepository, IUserProfileProvider profileProvider)
        {
            _projectProposalRepository = projectProposalRepository;
            _profileProvider = profileProvider;
        }

        protected override async Task Handle(UpdateProjectProposalCommand request, CancellationToken cancellationToken)
        {
            var user = _profileProvider.GetProfile().User;
            var projectProposal = await _projectProposalRepository.SingleOrDefaultAsync(ProjectProposalSpecification.GetById(request.Id, user.EventId),
                cancellationToken);

            if(projectProposal is null)
            {
                throw new NotFoundException();
            }
           
            projectProposal.UpdateData(request.UpdateProjectProposalDto);
            await _projectProposalRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}