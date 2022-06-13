using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workshop.Web.Dtos.Admin.ApiKey
{
    public class CreateKeyDto
    {
        public string Name { get; set; }
        public string UserType { get; set; }
    }
}
