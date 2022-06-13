using System;
using System.Collections.Generic;
using Workshop.Core.Domain.SharedKernel;
using Workshop.Core.Helpers;

namespace Workshop.Core.Domain.Model.Team
{
    public class Team : Entity<long>, IAggregateRoot, ITeam
    {
        public string Name { get; private set; }
        public TeamStatus TeamStatus { get; private set; }
        public DateTime CreationDate { get; private set; }
        public DateTime? TeamCompleteDate { get; private set; }
        public virtual ICollection<User.User> Users { get; private set; }
        public virtual ICollection<TeamReview.TeamReview> TeamReviews { get; private set; }

        public virtual ICollection<TeamCompetencyReview.TeamCompetencyReview> TeamCompetencyReviews
        {
            get;
            private set;
        }

        public long ProjectId { get; private set; }
        public Project.Project Project { get; private set; }

        public long? AuditoriumId { get; private set; }
        public virtual Auditorium.Auditorium Auditorium { get; set; }

        public long? ExpertId { get; private set; }
        public virtual User.User Expert { get; set; }
        public long EventId { get; private set; }
        public int? Mark { get; private set; }
        public string Comment { get; private set; }

        public bool IsEntried { get; private set; }

        public virtual TeamSlot.TeamSlot TeamSlot { get; private set; }
        
        public string PMSLink { get; set; }
        public string RepositoryLink { get; set; }
        public string AdditionalLink { get; set; }

        private Team()
        {
        }

        public Team(Project.Project project, long eventId)
        {
            Project = project;
            CreationDate = DateTimeHelper.GetCurrentTime();
            TeamStatus = TeamStatus.Incomplete;
            EventId = eventId;
            Users = new List<User.User>();
            TeamReviews = new List<TeamReview.TeamReview>();
            TeamCompetencyReviews = new List<TeamCompetencyReview.TeamCompetencyReview>();
            IsEntried = false;
        }

        public void StartTest()
        {
            TeamStatus = TeamStatus.TestWork;
        }

        public void UpdateTeamCompleteDate(DateTime completeDate)
        {
            TeamCompleteDate = completeDate;
        }

        public void IncompleteTeam()
        {
            TeamStatus = TeamStatus.Incomplete;
        }

        public void CompleteTest()
        {
            TeamStatus = TeamStatus.Complete;
        }

        public void Finish()
        {
            TeamStatus = TeamStatus.Finish;
        }

        public void ExpertDiscussion()
        {
            TeamStatus = TeamStatus.ExpertDiscussion;
        }

        public void UpdateExpert(long expertId)
        {
            ExpertId = expertId;
        }

        public void UpdateAuditorium(long? auditoriumId)
        {
            AuditoriumId = auditoriumId;
        }

        public void UpdateMark(int mark)
        {
            Mark = mark;
        }

        public void UpdateComment(string comment)
        {
            Comment = comment;
        }

        public void UpdateName(string name)
        {
            Name = name;
        }

        public void AddUser(User.User user)
        {
            Users.Add(user);
        }

        public void UpdateProject(Project.Project project)
        {
            Project = project;
        }

        public void UpdateTeamSlot(TeamSlot.TeamSlot slot)
        {
            TeamSlot = slot;
        }

        public void UpdateEntried(bool isEntried)
        {
            IsEntried = isEntried;
        }

        public void GetReadyForEntry()
        {
            TeamStatus = TeamStatus.ReadyForEntry;
        }

        public void UpdatePMSLink(string pmsLink)
        {
            PMSLink = pmsLink;
        }
        
        public void UpdateRepositoryLink(string repositoryLink)
        {
            RepositoryLink = repositoryLink;
        }
        
        public void UpdateAdditionalLink(string additionalLink)
        {
            AdditionalLink = additionalLink;
        }
    }
}