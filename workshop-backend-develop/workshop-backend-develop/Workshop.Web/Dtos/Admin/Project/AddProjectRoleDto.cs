using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workshop.Web.Dtos.Admin.Project
{
    public class AddProjectRoleDto
    {
        public string RoleName { get; set; }
        public long ProjectId { get; set; }
    }
}
