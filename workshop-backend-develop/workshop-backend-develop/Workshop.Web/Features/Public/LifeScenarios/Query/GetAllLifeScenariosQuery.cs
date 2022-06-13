using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Workshop.Core.Domain.Model.LifeScenario;
using Workshop.Core.Domain.Model.Session;
using Workshop.Core.Domain.SharedKernel.Repository;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Web.Dtos.Public.LifeScenario;

namespace Workshop.Web.Features.Public.LifeScenarios.Query
{
    public class GetAllLifeScenariosQuery : IRequest<ICollection<LifeScenarioDto>>
    {

    }

    public class GetAllLifeScenariosQueryHandler : IRequestHandler<GetAllLifeScenariosQuery, ICollection<LifeScenarioDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserProfileProvider _profileProvider;
        private readonly IReadOnlyRepository<LifeScenario> _lifeScenarioRepository;

        public GetAllLifeScenariosQueryHandler(IReadOnlyRepository<LifeScenario> lifeScenarioRepository,
            IMapper mapper, IUserProfileProvider userProfileProvider)
        {
            _mapper = mapper;
            _profileProvider = userProfileProvider;
            _lifeScenarioRepository = lifeScenarioRepository;
        }

        public async Task<ICollection<LifeScenarioDto>> Handle(GetAllLifeScenariosQuery request, CancellationToken cancellationToken)
        {
            var scenarios = await _lifeScenarioRepository.ListAsync(LifeScenarioSpecification.GetAll(_profileProvider.GetProfile().User.EventId), cancellationToken);
            var scenariosDtos = _mapper.Map<ICollection<LifeScenarioDto>>(scenarios);

            return scenariosDtos;
        }
    }
}
