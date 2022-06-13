using System.Collections.Generic;
using Workshop.Core.Domain.SharedKernel;

namespace Workshop.Core.Domain.Model.Project
{
    public class Project : Entity<long>, IAggregateRoot, IProject
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Curator { get; private set; }
        public string Organization { get; private set; }
        public string Contacts { get; private set; }
        public string Purpose { get; private set; }
        public string Result { get; private set; }
        public int TeamCapacity { get; private set; }
        public int MaxTeamCount { get; private set; }
        public bool IsAvailable { get; private set; }
        public bool IsPromoted { get; private set; }
        public bool IsEntryOpen { get; private set; }
        public long LifeScenarioId { get; private set; }
        public virtual LifeScenario.LifeScenario LifeScenario { get; private set; }
        public long KeyTechnologyId { get; private set; }
        public virtual KeyTechnology.KeyTechnology KeyTechnology { get; private set; }
        public virtual ICollection<ProjectCompetency.ProjectCompetency> Competencies { get; private set; }
        public virtual ICollection<Role.Role> Roles { get; private set; }

        public long EventId { get; set; }

        private Project()
        {
        }

        public Project(ProjectProposal.ProjectProposal proposal,
            long scenarioId,
            long technologyId)
        {
            Name = proposal.Name;
            Description = proposal.Description;
            Curator = proposal.Curator;
            Organization = proposal.Organization;
            Contacts = proposal.Contacts;
            Purpose = proposal.Purpose;
            Result = proposal.Result;
            TeamCapacity = proposal.TeamCapacity;
            LifeScenarioId = scenarioId;
            KeyTechnologyId = technologyId;
            Competencies = new List<ProjectCompetency.ProjectCompetency>();
            Roles = new List<Role.Role>();
            EventId = proposal.EventId;
            MaxTeamCount = proposal.MaxTeamCount;
            IsEntryOpen = false;
            IsAvailable = false;
            IsPromoted = false;
        }

        public Project(ProjectData projectData)
        {
            Name = projectData.ProjectName;
            Description = projectData.Description;
            Curator = projectData.Curator;
            Organization = projectData.Organization;
            Contacts = projectData.Contacts;
            Purpose = projectData.Purpose;
            Result = projectData.Result;
            TeamCapacity = projectData.TeamsSize;
            LifeScenarioId = projectData.LifeScenarioId;
            KeyTechnologyId = projectData.KeyTechnologyId;
            Competencies = new List<ProjectCompetency.ProjectCompetency>();
            Roles = new List<Role.Role>();
            EventId = projectData.EventId;
            MaxTeamCount = projectData.TeamLimit;
            IsAvailable = projectData.IsAvailable;
            IsPromoted = false;
            IsEntryOpen = false;
        }

        public void UpdateAvailability(bool availability)
        {
            IsAvailable = availability;
        }

        public void UpdateMaxTeamsCount(int maxTeamCount)
        {
            MaxTeamCount = maxTeamCount;
        }

        public void UpdateCompetencies(List<ProjectCompetency.ProjectCompetency> competencies)
        {
            Competencies = competencies;
        }

        public void SetProjectPromotion(bool value)
        {
            IsPromoted = value;
        }

        public void UpdateTextData(string name, string description = null, string curator = null,
            string organization = null, string contracts = null,
            string purpose = null, string result = null)
        {
            Name = name?.Trim();
            Description = description?.Trim();
            Curator = curator?.Trim();
            Organization = organization?.Trim();
            Contacts = contracts?.Trim();
            Purpose = purpose?.Trim();
            Result = result?.Trim();
        }

        public void IncrementMaxTeamsCount()
        {
            MaxTeamCount += 1;
        }

        public void DecrementMaxTeamsCount()
        {
            MaxTeamCount -= 1;
        }

        public void UpdateLifeScenario(long id)
        {
            LifeScenarioId = id;
        }

        public void UpdateKeyTechnology(long id)
        {
            KeyTechnologyId = id;
        }

        public void UpdateEntryOpen(bool value)
        {
            IsEntryOpen = value;
        }
    }
}