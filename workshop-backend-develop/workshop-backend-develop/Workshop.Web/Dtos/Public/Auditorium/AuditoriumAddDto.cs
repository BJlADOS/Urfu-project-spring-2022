using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workshop.Web.Dtos.Public.Auditorium
{
    public class AuditoriumAddDto
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public bool IsDefault { get; set; }
    }
}
