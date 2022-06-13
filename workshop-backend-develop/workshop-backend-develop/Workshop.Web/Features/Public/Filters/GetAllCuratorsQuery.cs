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
    public class GetAllCuratorsQuery : IRequest<ICollection<CuratorDto>>
    {
    }

    public class GetAllCuratorsQueryHandler : IRequestHandler<GetAllCuratorsQuery, ICollection<CuratorDto>>
    {
        private readonly IUserProfileProvider _profileProvider;
        private IReadOnlyRepository<Core.Domain.Model.Project.Project> _projectRepository;

        public GetAllCuratorsQueryHandler(IUserProfileProvider userProfileProvider, IReadOnlyRepository<Core.Domain.Model.Project.Project> projectRepository)
        {
            _profileProvider = userProfileProvider;
            _projectRepository = projectRepository;
        }

        public async Task<ICollection<CuratorDto>> Handle(GetAllCuratorsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.ListAsync(
                ProjectSpecification.GetAll(_profileProvider.GetProfile().User.EventId), cancellationToken);
            var curators = projects.Select(p => p.Curator).Distinct()
                .Select(value => new CuratorDto
                {
                    Name = value
                })
                .ToArray();
            return curators;
        }
    }
}
