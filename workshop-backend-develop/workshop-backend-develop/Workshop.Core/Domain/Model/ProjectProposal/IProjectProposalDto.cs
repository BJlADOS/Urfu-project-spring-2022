namespace Workshop.Core.Domain.Model.ProjectProposal
{
    public interface IProjectProposalDto
    {
        string Name { get; set; }
        string Description { get; set; }
        string Purpose { get; set; }
        string Result { get; set; }
        string Contacts { get; set; }
        string Curator { get; set; }
        string Organization { get; set; }
        int TeamCapacity { get; set; }
        int MaxTeamCount { get; set; }
        string LifeScenarioName { get; set; }
        string KeyTechnologyName { get; set; }

        ProposalStatus Status { get; set; }
    }
}