using Workshop.Core.Domain.Model.ProjectProposal;
using Workshop.Web.Dtos.Public.User;

namespace Workshop.Web.Dtos.Public.ProjectProposal
{
    public class ProposalDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Purpose { get; set; }
        public string Result { get; set; }
        public string Contacts { get; set; }
        public string Curator { get; set; }
        public string Organization { get; set; }
        public int TeamCapacity { get; set; }
        public int MaxTeamCount { get; set; }
        public string LifeScenarioName { get; set; }
        public string KeyTechnologyName { get; set; }
        public UsersListItemDto Author { get; set; }
        public ProposalStatus Status { get; set; }
    }
}