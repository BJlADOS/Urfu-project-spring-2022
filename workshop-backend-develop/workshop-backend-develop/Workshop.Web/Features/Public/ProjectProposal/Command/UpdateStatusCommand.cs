using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Workshop.Core.Domain.Model.ProjectProposal;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;
using Workshop.Web.Dtos.Public.ProjectProposal;
using Workshop.Web.Exceptions;

namespace Workshop.Web.Features.Public.ProjectProposal
{
    public class UpdateStatusCommand : IRequest
    {
        public long Id { get; set; }
        public UpdateStatusDto UpdateStatusDto { get; set; }
    }
    
    public class UpdateStatusCommandHandler : AsyncRequestHandler<UpdateStatusCommand>
    {
        private readonly IUserProfileProvider _profileProvider;
        private readonly ProjectProposalRepository _projectProposalRepository;

        public UpdateStatusCommandHandler(ProjectProposalRepository projectProposalRepository, IUserProfileProvider profileProvider)
        {
            _projectProposalRepository = projectProposalRepository;
            _profileProvider = profileProvider;
        }
        
        protected override async Task Handle(UpdateStatusCommand request,
            CancellationToken cancellationToken)
        {
            var user = _profileProvider.GetProfile().User;
            if (user.UserType != UserType.Admin)
                return;
            
            var projectProposal = await _projectProposalRepository.SingleOrDefaultAsync(ProjectProposalSpecification.GetById(request.Id, user.EventId),
                cancellationToken);
            if(projectProposal is null)
            {
                throw new NotFoundException();
            }
            projectProposal.UpdateStatus(request.UpdateStatusDto.Status);
            await _projectProposalRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}