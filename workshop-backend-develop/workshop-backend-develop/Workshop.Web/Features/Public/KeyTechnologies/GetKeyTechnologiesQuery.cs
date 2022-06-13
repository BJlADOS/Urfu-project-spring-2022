using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Workshop.Core.Domain.Model.KeyTechnology;
using Workshop.Core.Domain.Model.LifeScenario;
using Workshop.Core.Domain.SharedKernel.Repository;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Web.Dtos.Public.KeyTechnology;

namespace Workshop.Web.Features.Public.KeyTechnologies
{
    public class GetKeyTechnologiesQuery : IRequest<ICollection<KeyTechnologyDto>>
    {
        public long? LifeScenarioId { get; set; }
    }

    public class GetKeyTechnologiesQueryHandler : IRequestHandler<GetKeyTechnologiesQuery, ICollection<KeyTechnologyDto>>
    {
        private readonly IMapper _mapper;
        private readonly IReadOnlyRepository<LifeScenario> _lifeScenarioRepository;
        private readonly IReadOnlyRepository<KeyTechnology> _keyTechnologyRepository;
        private readonly IReadOnlyRepository<Workshop.Core.Domain.Model.Project.Project> _projectRepository;
        private IUserProfileProvider _profileProvider;

        public GetKeyTechnologiesQueryHandler(IMapper mapper,
            IReadOnlyRepository<LifeScenario> lifeScenarioRepository, IReadOnlyRepository<Core.Domain.Model.Project.Project> projectRepository,
            IUserProfileProvider profileProvider,
            IReadOnlyRepository<KeyTechnology> keyTechnologyRepository)
        {
            _mapper = mapper;
            _lifeScenarioRepository = lifeScenarioRepository;
            _projectRepository = projectRepository;
            _profileProvider = profileProvider;
            _keyTechnologyRepository = keyTechnologyRepository;
        }

        public async Task<ICollection<KeyTechnologyDto>> Handle(GetKeyTechnologiesQuery request, CancellationToken cancellationToken)
        {
            var eventId = _profileProvider.GetProfile().User.EventId;
            if (!request.LifeScenarioId.HasValue)
            {
                var allTechnologies = await _keyTechnologyRepository.ListAsync(KeyTechnologySpecification.GetAll(eventId), cancellationToken);
                return _mapper.Map<ICollection<KeyTechnologyDto>>(allTechnologies);
            }

            var scenario = await _lifeScenarioRepository.SingleAsync(LifeScenarioSpecification.GetById(request.LifeScenarioId.Value, eventId), cancellationToken);
            var projects = await _projectRepository.ListAsync(cancellationToken);
            var technologies = new List<KeyTechnology>();
            foreach (var project in projects)
            {
                if (project.LifeScenarioId == scenario.Key && !technologies.Select(t => t.Key).Contains(project.KeyTechnologyId))
                {
                    technologies.Add(project.KeyTechnology);
                }
            }
            technologies.Sort((first, second) => first.Key.CompareTo(second.Key));

            var keyTechnologyDtos = _mapper.Map<ICollection<KeyTechnologyDto>>(technologies);

            return keyTechnologyDtos;
        }
    }
}

