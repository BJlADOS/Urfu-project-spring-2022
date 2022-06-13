using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workshop.Core.Domain.Model.Competency;

namespace Workshop.Web.Dtos.Public.Competency
{
    public class CompetencyDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public CompetencyType CompetencyType { get; set; }
    }
}
