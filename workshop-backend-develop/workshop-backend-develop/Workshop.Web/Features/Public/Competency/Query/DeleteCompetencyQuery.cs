using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Core.Domain.Specification;
using Workshop.Infrastructure.Storage.Repositories;
using Workshop.Web.Dtos.Public.Competency;

namespace Workshop.Web.Features.Public.Competency.Query
{
    public class DeleteCompetencyQuery : IRequest
    {
        public long Id { get; set; }
    }

    public class DeleteCompetencyQueryHandler : AsyncRequestHandler<DeleteCompetencyQuery>
    {
        private CompetencyRepository _competencyRepository;
        public DeleteCompetencyQueryHandler(CompetencyRepository competencyRepository)
        {
            _competencyRepository = competencyRepository;
        }

        protected override async Task Handle(DeleteCompetencyQuery request, CancellationToken cancellationToken)
        {
            var competency = await _competencyRepository.SingleAsync(CompetencySpecification.GetById(request.Id), cancellationToken);
            await _competencyRepository.RemoveAsync(competency);
            await _competencyRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}
