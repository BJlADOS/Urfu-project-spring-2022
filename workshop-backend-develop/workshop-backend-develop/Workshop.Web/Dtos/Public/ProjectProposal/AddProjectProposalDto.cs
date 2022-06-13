using Workshop.Core.Domain.Model.ProjectProposal;

namespace Workshop.Web.Dtos.Public.ProjectProposal
{
    public class AddProjectProposalDto : IProjectProposalDto

    {
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
        
        public ProposalStatus Status { get; set; }
    }
}