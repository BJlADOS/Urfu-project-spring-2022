using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Workshop.Core.Domain.Model.KeyTechnology;
using Workshop.Core.Domain.Model.Project;

namespace Workshop.Infrastructure.Storage.Repositories
{
    public class KeyTechnologyRepository : EFRepository<KeyTechnology>, IKeyTechnologyRepository
    {
        public KeyTechnologyRepository(WorkshopDbContext context) : base(context)
        {
        }
    }
}
