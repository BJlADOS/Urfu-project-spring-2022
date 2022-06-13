using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Workshop.Core.Domain.Model.Auditorium;

namespace Workshop.Infrastructure.Storage.Repositories
{
    public class AuditoriumRepository : EFRepository<Auditorium>, IAuditoriumRepository
    {
        public AuditoriumRepository(WorkshopDbContext context) : base(context)
        {
        }

        protected override IQueryable<Auditorium> Items => base.Items
            .Include(x => x.Experts)
            .ThenInclude(x => x.User)
            .Include(x => x.TeamSlots)
            .ThenInclude(x => x.Team);
    }
}
