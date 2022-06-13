using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Workshop.Core.Domain.SharedKernel.Repository;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;
using Workshop.Web.Dtos.Public.Competency;
using Workshop.Web.Dtos.Public.Team;
using Workshop.Web.Dtos.Public.User;

namespace Workshop.Web.Features.Admin.User.Query
{
    public class UsersGetQuery : IRequest<ICollection<UsersListItemDto>>
    {
        public string Term { get; set; }
    }

    public class UsersGetQueryHandler : IRequestHandler<UsersGetQuery, ICollection<UsersListItemDto>>
    {
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;
        private IUserProfileProvider _profileProvider;

        public UsersGetQueryHandler(UserRepository userRepository, IMapper mapper, IUserProfileProvider profileProvider)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _profileProvider = profileProvider;
        }

        public async Task<ICollection<UsersListItemDto>> Handle(UsersGetQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.ShallowListAsync(
                UserSpecification.GetByTerm(request.Term, _profileProvider.GetProfile().User.EventId),
                cancellationToken);

            return users.Select(user => _mapper.Map<UsersListItemDto>(user)).ToList();
        }
    }
}
