using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Helpers;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;
using Workshop.Web.Dtos.Public.Competency;
using Workshop.Web.Dtos.Public.RequestProposal;
using Workshop.Web.Dtos.Public.User;

namespace Workshop.Web.Features.Public.RequestProposal.Query
{
    public class GetUsersFromRequestCommand :IRequest<ICollection<ShortUserRequestDto>>
    {
        public Core.Domain.Model.User.User Author { get; set; }
    }
    public class GetUsersFromRequestCommandHandler :IRequestHandler<GetUsersFromRequestCommand, ICollection<ShortUserRequestDto>>
    {
        private readonly RequestProposalRepository _requestProposalRepository;
        private readonly IMapper _mapper;
        private readonly TeamRepository _teamRepository;
        private readonly IUserProfileProvider _profile;
        private readonly IUserRepository _userRepository;

        public GetUsersFromRequestCommandHandler(
            RequestProposalRepository requests,
            IMapper mapper,
            TeamRepository teams,
            IUserProfileProvider profile,
            IUserRepository users
            )
        {
            _requestProposalRepository = requests;
            _mapper = mapper;
            _teamRepository = teams;
            _profile = profile;
            _userRepository = users;
        }

        public async Task<ICollection<ShortUserRequestDto>> Handle(GetUsersFromRequestCommand request, CancellationToken cancellationToken)
        {
            var eventId = request.Author.EventId;
              var proposals = await _requestProposalRepository.ListAsync(RequestProposalSpecification.GetByTeamleadId(request.Author.Key, eventId),cancellationToken);
            var users = await _userRepository.ListAsync(UserSpecification.GetByIds(proposals.Select(x => x.UserId).ToHashSet()), cancellationToken);
            var proposalDict = proposals.ToNoneDuplicatedDictionary(key=>key.UserId,val => val);
            var userDtos = new List<ShortUserRequestDto>();
            
            foreach(var user in users)
            {
                var userDto = _mapper.Map<ShortUserRequestDto>(user);
                userDto.RoleName = proposalDict[user.Key].RoleName;
                userDto.Status = proposalDict[user.Key].Status;
                userDtos.Add(userDto);
            }

            return userDtos;
        }
    }
}
