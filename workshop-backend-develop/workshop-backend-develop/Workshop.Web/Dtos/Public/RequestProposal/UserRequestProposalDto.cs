using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workshop.Web.Dtos.Public.Team;
using Workshop.Web.Dtos.Public.User;

namespace Workshop.Web.Dtos.Public.RequestProposal
{
    public class UserRequestProposalDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Contacts { get; set; }
        public string Purpose { get; set; }
        public string Result { get; set; }
        public int TeamCapacity { get; set; }
        public string LifeScenarioName { get; set; }
        public string KeyTechnologyName { get; set; }
        public string Description { get; set; }

        public string RoleName { get; set; }

        public ShortTeamDto Team { get; set; }
        public Core.Domain.Model.RequestProposal.RequestStatus RequestStatus {get;set;}

    }
}
