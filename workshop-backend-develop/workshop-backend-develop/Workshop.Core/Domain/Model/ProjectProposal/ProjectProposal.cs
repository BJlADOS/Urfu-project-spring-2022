using System;
using Workshop.Core.Domain.SharedKernel;
using Workshop.Core.Helpers;

namespace Workshop.Core.Domain.Model.ProjectProposal
{
    public class ProjectProposal : Entity<long>, IAggregateRoot, IProjectProposal
    {
        public long EventId { get; private set; }
        public long AuthorId { get; private set; }
        public User.User Author { get; set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Purpose { get; private set; }
        public string Result { get; private set; }
        public string Contacts { get; private set; }
        public string Curator { get; private set; }
        public string Organization { get; private set; }
        public int TeamCapacity { get; private set; }
        public int MaxTeamCount { get; private set; }
        public string LifeScenarioName { get; private set; }
        public string KeyTechnologyName { get; private set; }
        public DateTime CreationDate { get; private set; }

        public ProposalStatus Status { get; private set; }

        public ProjectProposal(long eventId, long authorId, string name, string description,
            string purpose, string result, string contacts, string curator,
            string organization, int teamCapacity, int maxTeamCount,
            string lifeScenarioName, string keyTechnologyName,
            DateTime creationDate, ProposalStatus status)
        {
            EventId = eventId;
            AuthorId = authorId;
            Name = name;
            Description = description;
            Purpose = purpose;
            Result = result;
            Contacts = contacts;
            Curator = curator;
            Organization = organization;
            TeamCapacity = teamCapacity;
            MaxTeamCount = maxTeamCount;
            LifeScenarioName = lifeScenarioName;
            KeyTechnologyName = keyTechnologyName;
            CreationDate = creationDate;
            Status = status;
        }

        public ProjectProposal(User.User author, IProjectProposalDto dto)
        {
            EventId = author.EventId;
            AuthorId = author.Key;
            Name = dto.Name;
            Description = dto.Description;
            Purpose = dto.Purpose;
            Result = dto.Result;
            Contacts = dto.Contacts;
            Curator = dto.Curator;
            Organization = dto.Organization;
            TeamCapacity = dto.TeamCapacity;
            MaxTeamCount = dto.MaxTeamCount;
            LifeScenarioName = dto.LifeScenarioName;
            KeyTechnologyName = dto.KeyTechnologyName;
            CreationDate = DateTimeHelper.GetCurrentTime();
            Status = ProposalStatus.Pending;
        }

        public void UpdateData(IProjectProposalDto dto)
        {
            Name = dto.Name;
            Description = dto.Description;
            Purpose = dto.Purpose;
            Result = dto.Result;
            Contacts = dto.Contacts;
            Curator = dto.Curator;
            Organization = dto.Organization;
            TeamCapacity = dto.TeamCapacity;
            MaxTeamCount = dto.MaxTeamCount;
            LifeScenarioName = dto.LifeScenarioName;
            KeyTechnologyName = dto.KeyTechnologyName;
            Status = dto.Status;
        }

        public void UpdateData(ICreateProjectFromProposalDto dto)
        {
            Name = dto.Name;
            Description = dto.Description;
            Purpose = dto.Purpose;
            Result = dto.Result;
            Contacts = dto.Contacts;
            Curator = dto.Curator;
            Organization = dto.Organization;
            TeamCapacity = dto.TeamCapacity;
            LifeScenarioName = dto.LifeScenarioName;
            KeyTechnologyName = dto.KeyTechnologyName;
            Status = dto.Status;
        }

        public void UpdateStatus(ProposalStatus status)
        {
            Status = status;
        }
    }
}