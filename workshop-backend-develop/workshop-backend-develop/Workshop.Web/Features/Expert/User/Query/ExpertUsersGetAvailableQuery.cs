using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Domain.SharedKernel.Repository;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Web.Dtos.Expert.User;

namespace Workshop.Web.Features.Public.ExpertUser.Query
{
    public class ExpertUsersGetAvailableQuery : IRequest<ICollection<ExpertUserDto>>
    {
    }

    public class ExpertUserGetAvailableQueryHandler : IRequestHandler<ExpertUsersGetAvailableQuery, ICollection<ExpertUserDto>>
    {

        private readonly IReadOnlyRepository<Core.Domain.Model.User.User> _userRepository;
        private readonly IMapper _mapper;
        private IUserProfileProvider _profileProvider;

        public ExpertUserGetAvailableQueryHandler(IUserRepository userRepository, IMapper mapper, IUserProfileProvider profileProvider)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _profileProvider = profileProvider;
        }

        public async Task<ICollection<ExpertUserDto>> Handle(ExpertUsersGetAvailableQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.ListAsync(UserSpecification.GetAvailableExperts(_profileProvider.GetProfile().User.EventId), cancellationToken);
            var usersDto = _mapper.Map<ICollection<ExpertUserDto>>(users);
            return usersDto;
        }
    }
}
