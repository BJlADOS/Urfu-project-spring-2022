using System.Linq;
using Microsoft.EntityFrameworkCore;
using Workshop.Core.Domain.Model.Role;

namespace Workshop.Infrastructure.Storage.Repositories
{
    public class RoleRepository : EFRepository<Role>, IRoleRepository
    {
        public RoleRepository(WorkshopDbContext context) : base(context)
        {
            
        }
    }
}