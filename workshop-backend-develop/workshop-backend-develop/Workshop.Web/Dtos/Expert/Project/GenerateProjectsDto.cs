using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Workshop.Web.Dtos.Expert.Project
{
    public class GenerateProjectsDto
    {
        public long EventId { get; set; }
        public IFormFile File { get; set; }
        public bool ResetProjects { get; set; }
        public bool CloseProjects { get; set; }
    }
}
