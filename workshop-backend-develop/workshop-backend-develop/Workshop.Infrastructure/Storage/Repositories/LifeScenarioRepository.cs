using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Workshop.Core.Domain.Model.LifeScenario;
using Workshop.Core.Domain.Model.Project;

namespace Workshop.Infrastructure.Storage.Repositories
{
    public class LifeScenarioRepository : EFRepository<LifeScenario>, ILifeScenarioRepository
    {
        public LifeScenarioRepository(WorkshopDbContext context) : base(context)
        {
        }

        protected override IQueryable<LifeScenario> Items => base.Items;
    }
}
