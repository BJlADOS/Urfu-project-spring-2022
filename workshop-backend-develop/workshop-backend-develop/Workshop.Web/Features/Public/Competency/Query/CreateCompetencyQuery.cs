using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Core.Domain.Model.Competency;
using Workshop.Infrastructure.Storage.Repositories;

namespace Workshop.Web.Features.Public.Competency.Query
{
    public class CreateCompetencyQuery : IRequest
    {
        public string Name { get; set; }
        public CompetencyType CompetencyType { get; set; }
    }

    public class CreateCompetencyQueryHandler : AsyncRequestHandler<CreateCompetencyQuery>
    {
        private CompetencyRepository _competencyRepository;
        public CreateCompetencyQueryHandler(CompetencyRepository competencyRepository)
        {
            _competencyRepository = competencyRepository;
        }

        protected override async Task Handle(CreateCompetencyQuery request, CancellationToken cancellationToken)
        {
            var competency = new Core.Domain.Model.Competency.Competency(request.Name, request.CompetencyType);
            await _competencyRepository.AddAsync(competency, cancellationToken);
            await _competencyRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}
