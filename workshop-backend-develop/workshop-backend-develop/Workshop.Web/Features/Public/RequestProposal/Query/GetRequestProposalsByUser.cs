using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Workshop.Core.Domain.Model.Project;
using Workshop.Core.Domain.Model.Team;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;
using Workshop.Web.Dtos.Public.RequestProposal;
using Workshop.Web.Exceptions;

namespace Workshop.Web.Features.Public.RequestProposal.Query
{
    public class GetRequestProposalsByUser : IRequest<ICollection<UserRequestProposalDto>>
    {
        public long Id { get; set; }
    }

    public class GetRequestProposalByIdHandler : IRequestHandler<GetRequestProposalsByUser, ICollection<UserRequestProposalDto>>
    {
        private readonly RequestProposalRepository _requestProposalRepository;
        private readonly IMapper _mapper;
        private readonly UserRepository _userRepository;

        private readonly IUserProfileProvider _profile;

        public GetRequestProposalByIdHandler(
            RequestProposalRepository requestProposalRepository,
            IMapper mapper,
            IUserProfileProvider provider,
            UserRepository users)
        {
            _requestProposalRepository = requestProposalRepository;
            _mapper = mapper;
            _profile = provider;
            _userRepository = users;
        }

        public async Task<ICollection<UserRequestProposalDto>> Handle(GetRequestProposalsByUser request,
            CancellationToken cancellationToken)
        {
            var eventId = _profile.GetProfile().User.EventId;
            var proposals = await _requestProposalRepository.ListAsync(
                RequestProposalSpecification.GetByUserId(request.Id,eventId),
                cancellationToken);


            var teamleads = await _userRepository.ShallowListAsyncWithTeams(UserSpecification.GetByIds(proposals.Select(x => x.TeamleadId).ToHashSet()), cancellationToken);
            var projects = teamleads.Where(x=>x.Team!=null).Select(x => x.Team.Project).ToArray();
            var teams = teamleads.Where(x=>x.Team!=null).Select(x => x.Team).ToArray();
           
            return teams
                .SelectMany(u => proposals.SelectMany(p =>
                    projects
                        .Select(x =>
                        _mapper.Map(x, _mapper.Map(p, _mapper.Map<UserRequestProposalDto>(u)))
                        )
                   )
                ).ToArray();
        }
    }
}