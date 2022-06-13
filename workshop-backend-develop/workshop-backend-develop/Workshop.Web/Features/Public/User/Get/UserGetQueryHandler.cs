using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Domain.Specification;
using Workshop.Web.Dtos.Public.Competency;
using Workshop.Web.Dtos.Public.User;
using Workshop.Web.Features.Public.User.Query;

namespace Workshop.Web.Features.Public.User.Get
{
    public class UserGetQueryHandler : IRequestHandler<UserGetQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;


        public UserGetQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(UserGetQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.SingleAsync(UserSpecification.GetById(request.Id), cancellationToken);
            var competencies =_mapper.Map<ICollection<UserCompetencyDto>>(user.Competencies);
            var userDto = _mapper.Map<UserDto>(user);
            userDto.Competencies = competencies;
            return userDto;
        }
    }
}
