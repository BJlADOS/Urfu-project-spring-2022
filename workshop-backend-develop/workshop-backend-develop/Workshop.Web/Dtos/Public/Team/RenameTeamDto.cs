using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workshop.Web.Dtos.Public.Team
{
    public class RenameTeamDto
    {
        public long TeamId { get; set; }
        public string Name { get; set; }
    }
}
