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
using Workshop.Web.Dtos.Public.Auditorium;

namespace Workshop.Web.Features.Public.Auditorium.Query
{
    public class AuditoriumsGetCommand : IRequest<ICollection<AuditoriumDto>>
    {
    }

    public class AuditoriumsGetCommandHandler : IRequestHandler<AuditoriumsGetCommand, ICollection<AuditoriumDto>>
    {
        private IReadOnlyRepository<Core.Domain.Model.Auditorium.Auditorium> _auditoriumRepository;
        private IUserProfileProvider _profileProvider;
        private IMapper _mapper;

        public AuditoriumsGetCommandHandler(IReadOnlyRepository<Core.Domain.Model.Auditorium.Auditorium> auditoriumRepository, IMapper mapper, IUserProfileProvider profileProvider)
        {
            _auditoriumRepository = auditoriumRepository;
            _mapper = mapper;
            _profileProvider = profileProvider;
        }

        public async Task<ICollection<AuditoriumDto>> Handle(AuditoriumsGetCommand request, CancellationToken cancellationToken)
        {
            var auditoriums = await _auditoriumRepository.ListAsync(AuditoriumSpecification.GetAll(_profileProvider.GetProfile().User.EventId), cancellationToken);
            var auditoriumDtos = _mapper.Map<ICollection<AuditoriumDto>>(auditoriums);
            return auditoriumDtos;
        }
    }
}
