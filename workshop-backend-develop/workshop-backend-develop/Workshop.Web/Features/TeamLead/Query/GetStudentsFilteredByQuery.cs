using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;
using Workshop.Web.Dtos.Public.User;

namespace Workshop.Web.Features.TeamLead
{
    public class GetStudentsFilteredByQuery : IRequest<List<ShortUserDto>>
    {
        public string Term { get; set; }
        public List<long> CompetenciesIds { get; set; }
        public int PageNumber { get; set; }

        public long? LastElement { get; set; }
    }

    public class
        GetStudentsFilteredByQueryHandler : IRequestHandler<GetStudentsFilteredByQuery, List<ShortUserDto>>
    {
        private readonly IMapper _mapper;
        private readonly UserRepository _userRepository;
        private readonly IUserProfileProvider _profileProvider;
        private readonly CompetencyRepository _competencyRepository;
        private readonly RequestProposalRepository _requestRepository;
        private readonly UserCompetencyRepository _userCompetency;

        public GetStudentsFilteredByQueryHandler(
            IMapper mapper, UserRepository userRepository, CompetencyRepository competencyRepository,
            IUserProfileProvider profileProvider, RequestProposalRepository requests,
            UserCompetencyRepository competencies)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _profileProvider = profileProvider;
            _competencyRepository = competencyRepository;
            _requestRepository = requests;
            _userCompetency = competencies;
        }

        public async Task<List<ShortUserDto>> Handle(GetStudentsFilteredByQuery request,
            CancellationToken cancellationToken)
        {
            var filters = request;
            var counter = filters?.CompetenciesIds?.Count ?? 0;
            var teamLead = _profileProvider.GetProfile().User;

            var competencies = Enumerable
                .Range(0, counter)
                .Select(async i => await _competencyRepository.SingleAsync(
                    CompetencySpecification.GetById(filters.CompetenciesIds[i]),
                    cancellationToken))
                .Select(task => task.Result)
                .ToHashSet();

            const int pageSize = 50;

            return request.LastElement is null
                ? (from user in await _userRepository.ShallowListAsyncWithCompetencies(
                        UserSpecification.GetUsersByTermWithUserType(request.Term, teamLead.EventId, UserType.Student),
                        cancellationToken)
                    let userDto = _mapper.Map<ShortUserDto>(user)
                    where competencies
                        .All(user.Competencies
                            .Where(us =>
                                us.UserCompetencyType == Core.Domain.Model.UserCompetency.UserCompetencyType.Current)
                            .Select(userCompetency => userCompetency.Competency)
                            .ToHashSet().Contains)
                    select userDto).Skip((request.PageNumber - 1) * pageSize).Take(pageSize).ToList()
                : (from user in await _userRepository.ShallowListAsyncWithCompetencies(
                        UserSpecification.GetUsersByTermWithUserType(request.Term, teamLead.EventId, UserType.Student),
                        cancellationToken)
                    let userDto = _mapper.Map<ShortUserDto>(user)
                    where user.Key > request.LastElement && competencies
                        .All(user.Competencies
                            .Where(us =>
                                us.UserCompetencyType == Core.Domain.Model.UserCompetency.UserCompetencyType.Current)
                            .Select(userCompetency => userCompetency.Competency)
                            .ToHashSet().Contains)
                    select userDto).Take(pageSize).ToList();
        }
    }
}