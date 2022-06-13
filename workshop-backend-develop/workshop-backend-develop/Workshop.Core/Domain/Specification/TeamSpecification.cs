using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using Workshop.Core.Domain.Model.Team;
using Workshop.Core.Domain.SharedKernel.Specification;

namespace Workshop.Core.Domain.Specification
{
    public static class TeamSpecification
    {
        public static ISpecification<Team> GetById(long? id, long eventId) =>
            new Specification<Team>(u => id.HasValue && u.EventId == eventId && u.Key == id );

        public static ISpecification<Team> GetEntriedByProjectId(long projectId, long eventId) =>
            new Specification<Team>(t => t.EventId == eventId && t.ProjectId == projectId && t.IsEntried);

        public static ISpecification<Team> GetProjectTeams(long projectId, long eventId) =>
            new Specification<Team>(u =>
                u.EventId == eventId && u.ProjectId == projectId);

        public static ISpecification<Team> GetTeamsByAuditoriumId(long eventId, long auditoriumId) =>
            new Specification<Team>(u =>
                u.EventId == eventId && u.TeamSlot.AuditoriumId == auditoriumId);

        public static ISpecification<Team> GetTeamsWithAuditorium(long eventId) =>
            new Specification<Team>(u => u.EventId == eventId && u.AuditoriumId.HasValue);

        public static ISpecification<Team> GetAll(long eventId) => new Specification<Team>(x => x.EventId == eventId);
        public static ISpecification<Team> GetAllCompleted(long eventId) =>
            new Specification<Team>(x => x.EventId == eventId && x.TeamStatus != TeamStatus.Incomplete);

        public static ISpecification<Team> GetAllRegistred(long eventId) =>
            new Specification<Team>(x => x.EventId == eventId && x.TeamStatus == TeamStatus.Complete);

        public static ISpecification<Team> GetNotEmpty(long eventId) =>
            new Specification<Team>(x => x.EventId == eventId && x.Users != null && x.Users.Count > 0);

        public static ISpecification<Team> GetFillTeams(long projectId, long eventId) =>
            new Specification<Team>(x =>
                x.EventId == eventId && x.ProjectId == projectId && x.TeamStatus != TeamStatus.Incomplete && x.IsEntried);

        public static ISpecification<Team> GetCountByTeamsEntried(long projectId, long eventId) =>
            new Specification<Team>(p => p.EventId == eventId && p.ProjectId == projectId && p.IsEntried);
    }
}