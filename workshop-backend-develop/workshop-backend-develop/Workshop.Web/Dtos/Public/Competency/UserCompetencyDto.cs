using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workshop.Core.Domain.Model.Competency;
using Workshop.Core.Domain.Model.UserCompetency;

namespace Workshop.Web.Dtos.Public.Competency
{
    public class UserCompetencyDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public CompetencyType CompetencyType { get; set; }
        public UserCompetencyType UserCompetencyType { get; set; }
    }
}
