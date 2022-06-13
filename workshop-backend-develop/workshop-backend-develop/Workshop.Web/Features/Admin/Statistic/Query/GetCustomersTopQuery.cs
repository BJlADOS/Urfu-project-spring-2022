using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Workshop.Core.Domain.Model.Project;
using Workshop.Core.Domain.SharedKernel.Repository;
using Workshop.Core.Domain.Specification;
using Workshop.Infrastructure.Storage.Repositories;
using Workshop.Web.Dtos.Admin.Statistic;

namespace Workshop.Web.Features.Admin.Statistic.Query
{
    public class GetCustomersTopQuery : IRequest<ICollection<CustomersTopDto>>
    {
        public long EventId { get; set; }
    }

    public class GetCustomersTopQueryHandler : IRequestHandler<GetCustomersTopQuery, ICollection<CustomersTopDto>>
    {
        private readonly IReadOnlyRepository<Core.Domain.Model.Project.Project> _projectRepository;

        public GetCustomersTopQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ICollection<CustomersTopDto>> Handle(GetCustomersTopQuery request,
            CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.ListAsync(
                ProjectSpecification.GetAll(request.EventId), cancellationToken);
            
            var customers = projects.GroupBy(p => p.Organization)
                .Select(item => new CustomersTopDto
                {
                    Name = item.Key,
                    ProjectsCount = item.Count()
                })
                .OrderByDescending(c => c.ProjectsCount)
                .ToArray();

            return customers;
        }
    }
}