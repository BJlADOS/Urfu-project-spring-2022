using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Core.Domain.SharedKernel.Repository;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Web.Dtos.Public.Filters;

namespace Workshop.Web.Features.Public.Filters
{
    public class GetAllCustomersQuery : IRequest<ICollection<CustomerDto>>
    {
    }

    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, ICollection<CustomerDto>>
    {
        private readonly IUserProfileProvider _profileProvider;
        private IReadOnlyRepository<Core.Domain.Model.Project.Project> _projectRepository;

        public GetAllCustomersQueryHandler(IUserProfileProvider userProfileProvider, IReadOnlyRepository<Core.Domain.Model.Project.Project> projectRepository)
        {
            _profileProvider = userProfileProvider;
            _projectRepository = projectRepository;
        }

        public async Task<ICollection<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.ListAsync(
                ProjectSpecification.GetAll(_profileProvider.GetProfile().User.EventId), cancellationToken);
            var customers = projects.Select(p => p.Organization).Distinct()
                .Select(value => new CustomerDto
                {
                    Name = value
                })
                .ToArray();
            return customers;
        }
    }
}
