using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Domain.SharedKernel.Repository;
using Workshop.Core.Domain.Specification;
using Workshop.Web.Dtos.Expert.User;

namespace Workshop.Web.Features.Public.ExpertUser.Query
{
    public class ExpertUserGetQuery : IRequest<ExpertUserDto>
    {
        public long Id { get; set; }
    }

    public class ExpertUserGetQueryHandler : IRequestHandler<ExpertUserGetQuery, ExpertUserDto>
    {

        private readonly IReadOnlyRepository<Core.Domain.Model.User.User> _userRepository;
        private readonly IMapper _mapper;
        
        public ExpertUserGetQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ExpertUserDto> Handle(ExpertUserGetQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.SingleAsync(UserSpecification.GetById(request.Id), cancellationToken);
            var userDto = _mapper.Map<ExpertUserDto>(user);
            return userDto;
        }
    }
}
