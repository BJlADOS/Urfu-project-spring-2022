using System.Linq;
using Microsoft.EntityFrameworkCore;
using Workshop.Core.Domain.Model.TeamSlot;

namespace Workshop.Infrastructure.Storage.Repositories
{
    public class TeamSlotsRepository : EFRepository<TeamSlot>, ITeamSlotsRepository
    {
        protected override IQueryable<TeamSlot> Items => base.Items
            .Include(x => x.Team);
        
        public TeamSlotsRepository(WorkshopDbContext context) : base(context)
        {
        }
    }
}