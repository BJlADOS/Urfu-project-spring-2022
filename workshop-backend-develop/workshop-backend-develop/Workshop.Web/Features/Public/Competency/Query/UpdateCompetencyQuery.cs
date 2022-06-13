using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Core.Domain.Model.Competency;
using Workshop.Core.Domain.Specification;
using Workshop.Infrastructure.Storage.Repositories;

namespace Workshop.Web.Features.Public.Competency.Query
{
    public class UpdateCompetencyQuery : IRequest
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public CompetencyType CompetencyType { get; set; }
    }

    public class UpdateCompetencyQueryHandler : AsyncRequestHandler<UpdateCompetencyQuery>
    {
        private CompetencyRepository _competencyRepository;
        public UpdateCompetencyQueryHandler(CompetencyRepository competencyRepository)
        {
            _competencyRepository = competencyRepository;
        }

        protected override async Task Handle(UpdateCompetencyQuery request, CancellationToken cancellationToken)
        {
            var competency = await _competencyRepository.SingleAsync(CompetencySpecification.GetById(request.Id), cancellationToken);
            competency.UpdateName(request.Name);
            competency.UpdateCompetencyType(request.CompetencyType);
            await _competencyRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}
