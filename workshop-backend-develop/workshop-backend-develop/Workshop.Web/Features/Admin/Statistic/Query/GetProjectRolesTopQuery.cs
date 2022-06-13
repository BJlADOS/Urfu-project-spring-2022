using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Workshop.Core.Domain.Model.Role;

namespace Workshop.Web.Features.Admin.Statistic.Query
{
    public class GetProjectRolesTopQuery : IRequest<ICollection<string>>
    {
        public long EventId { get; set; }
    }

    public class GetProjectRolesTopQueryHandler : IRequestHandler<GetProjectRolesTopQuery, ICollection<string>>
    {
        private readonly IRoleRepository _roleRepository;

        public GetProjectRolesTopQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<ICollection<string>> Handle(GetProjectRolesTopQuery request,
            CancellationToken cancellationToken)
        {
            var roles = await _roleRepository
                .Query(r => r.Include(u => u.Project), cancellationToken);

            var memberRoles = roles.Where(r => r.Project.EventId == request.EventId)
                .GroupBy(r => r.Name)
                .OrderByDescending(r => r.Count())
                .Select(r => r.Key)
                .ToArray();

            return memberRoles;
        }
    }
}