using System;
using System.ComponentModel.Design;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Workshop.Core.Domain.Model.Team;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Web.Exceptions;

namespace Workshop.Web.Features.Public.Team.Command
{
    public class UpdateLinksCommand : IRequest
    {
        public long TeamId { get; set; }
        public string PMSLink { get; set; }
        public string RepositoryLink { get; set; }
        public string AdditionalLink { get; set; }
    }

    public class UpdateLinksCommandHandler : AsyncRequestHandler<UpdateLinksCommand>
    {
        private ITeamRepository _teamRepository;
        private IUserProfileProvider _profileProvider;

        public UpdateLinksCommandHandler(ITeamRepository teamRepository, IUserProfileProvider profileProvider)
        {
            _teamRepository = teamRepository;
            _profileProvider = profileProvider;
        }

        private bool CheckLink(string link)
        {
            var regex = @"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$";
            return Regex.IsMatch(link, regex);
        }
        
        protected override async Task Handle(UpdateLinksCommand request, CancellationToken cancellationToken)
        {
            var pmsLink = request.PMSLink.Trim();
            var repositoryLink = request.RepositoryLink.Trim();
            var additionalLink = request.AdditionalLink.Trim();

            if (!(CheckLink(pmsLink) || string.IsNullOrEmpty(pmsLink))
                || !(CheckLink(repositoryLink) || string.IsNullOrEmpty(repositoryLink))
                || !(CheckLink(additionalLink) || string.IsNullOrEmpty(additionalLink)))
                throw new ArgumentException();

            var user = _profileProvider.GetProfile().User;

            var team = await _teamRepository.SingleOrDefaultAsync(
                TeamSpecification.GetById(request.TeamId, user.EventId),
                cancellationToken);

            if (team == null)
                throw new NotFoundException();
            
            if (team.TeamStatus != TeamStatus.Complete 
                || (user.TeamId != request.TeamId 
                    && (user.UserType != UserType.Student || user.UserType != UserType.Teamlead)))
            {
                throw new ForbiddenException();
            }

            team.UpdatePMSLink(pmsLink);
            team.UpdateRepositoryLink(repositoryLink);
            team.UpdateAdditionalLink(additionalLink);

            await _teamRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}