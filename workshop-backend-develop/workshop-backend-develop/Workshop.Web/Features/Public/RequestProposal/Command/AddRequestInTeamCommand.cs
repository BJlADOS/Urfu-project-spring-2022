using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Core.Domain.Model.Team;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;
using Workshop.Web.Exceptions;

namespace Workshop.Web.Features.Public.RequestProposal.Command
{
    public class AddRequestInTeamCommand : IRequest
    {
        public long UserId { get; set; }
        public long TeamId { get; set; }

        public long ProjectId { get; set; }

        public string RoleName { get; set; }
    }
    public class AddRequestInTeamCommandHandler : AsyncRequestHandler<AddRequestInTeamCommand>
    {
        private readonly RequestProposalRepository _requestProposalRepository;
        private readonly IUserProfileProvider _profileProvider;
        private readonly ITeamRepository _teamRepository;

        public AddRequestInTeamCommandHandler(RequestProposalRepository requestProposalRepository,
            IUserProfileProvider provider,
            ITeamRepository teamRepository)
        {
                _requestProposalRepository = requestProposalRepository;
            _profileProvider = provider;
            _teamRepository = teamRepository;
        }

        protected override async Task Handle(AddRequestInTeamCommand request, CancellationToken cancellationToken)
        {
            var eventId = _profileProvider.GetProfile().User.EventId;
            var team = await _teamRepository.FirstOrDefaultAsync(TeamSpecification.GetById(request.TeamId, eventId), cancellationToken);
            if(team is null)
            {
                throw new NotFoundException();
            }
            var teamlead = team.Users.FirstOrDefault(x=>x.UserType==Core.Domain.Model.User.UserType.Teamlead);
            if (teamlead is null || await _requestProposalRepository.FirstOrDefaultAsync(RequestProposalSpecification.GetByTeamLeadAndUserId(teamlead.Key, request.UserId,eventId), cancellationToken) !=null)
            {
                throw new ForbiddenException();
            }
            var proposal = new Core.Domain.Model.RequestProposal.RequestProposal(request.UserId,
                teamlead.Key,
                request.ProjectId,
                eventId,
                Core.Domain.Model.RequestProposal.RequestStatus.Accepted,
                request.RoleName);


                await _requestProposalRepository.AddAsync(proposal, cancellationToken);
                await _requestProposalRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            
        }


    }
}
