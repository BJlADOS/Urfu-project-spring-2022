using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Workshop.Core.Domain.SharedKernel.Repository;
using Workshop.Core.Domain.Specification;
using Workshop.Web.Dtos.Public.Competency;

namespace Workshop.Web.Features.Public.Competency.Query
{
    public class CompetenciesGetQuery : IRequest<ICollection<CompetencyDto>>
    {
        public string Term { get; set; }
    }

    public class CompetenciesGetQueryHandler : IRequestHandler<CompetenciesGetQuery, ICollection<CompetencyDto>>
    {
        private IReadOnlyRepository<Core.Domain.Model.Competency.Competency> _competencyRepository;
        private IMapper _mapper;

        public CompetenciesGetQueryHandler(IReadOnlyRepository<Core.Domain.Model.Competency.Competency> competencyRepository, IMapper mapper)
        {
            _competencyRepository = competencyRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<CompetencyDto>> Handle(CompetenciesGetQuery request, CancellationToken cancellationToken)
        {
            var competencies = await _competencyRepository.ListAsync(CompetencySpecification.GetByTerm(request.Term), cancellationToken);
            return _mapper.Map<ICollection<CompetencyDto>>(competencies);
        }
    }
}
