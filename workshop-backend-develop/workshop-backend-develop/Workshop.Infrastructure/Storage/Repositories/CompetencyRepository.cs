using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop.Core.Domain.Model.Competency;
using Workshop.Core.Domain.Model.Project;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Domain.Model.UserCompetency;

namespace Workshop.Infrastructure.Storage.Repositories
{
    public class CompetencyRepository : EFRepository<Competency>, ICompetencyRepository
    {
        public CompetencyRepository(WorkshopDbContext context) : base(context)
        {
        }
    }
}
