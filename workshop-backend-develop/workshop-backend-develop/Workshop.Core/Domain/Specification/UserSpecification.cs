using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Workshop.Core.Domain.Model.Competency;
using Workshop.Core.Domain.Model.Team;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Domain.Model.UserCompetency;
using Workshop.Core.Domain.SharedKernel.Specification;

namespace Workshop.Core.Domain.Specification
{
    public static class UserSpecification
    {
        public static ISpecification<User> GetById(long id) => new Specification<User>(u => u.Key == id);
        public static ISpecification<User> GetByLogin(string login) => new Specification<User>(x => x.Login == login);

        public static ISpecification<User> GetByLoginWithEvent(string login, long eventId) =>
            new Specification<User>(x => x.Login == login && x.EventId == eventId);

        public static ISpecification<User> GetByIds(HashSet<long> users) =>
            new Specification<User>(x => users.Contains(x.Key));

        public static ISpecification<User> GetByTerm(string term, long eventId) => new Specification<User>(u =>
            u.EventId == eventId
            && (string.IsNullOrWhiteSpace(term)
                || u.Key.ToString().Contains(term)
                || u.Login.Contains(term)
                || (u.FirstName + " " + u.LastName).Contains(term)
                || (u.LastName + " " + u.FirstName).Contains(term)
                || u.MiddleName.Contains(term)
                || u.PhoneNumber.Contains(term)
                || u.Email.Contains(term)));

        public static ISpecification<User> GetUsersByTermWithUserType(string term, long eventId, UserType userType) =>
            GetByTerm(term, eventId).And(GetUserWithUserType(userType));

        public static ISpecification<User> GetUserWithUserType(UserType userType) =>
            new Specification<User>(u => u.ProfileFilled && !u.TeamId.HasValue && u.UserType == userType);

        public static ISpecification<User> GetAvailableExperts(long eventId) => new Specification<User>(u =>
            u.EventId == eventId && u.UserType == UserType.Expert);

        public static ISpecification<User> GetByTeam(long? teamId, long eventId) => new Specification<User>(u =>
            u.EventId == eventId && u.TeamId.HasValue && u.TeamId.Value == teamId &&
            (u.UserType == UserType.Student || u.UserType == UserType.Teamlead));

        public static ISpecification<User> GetAllStudents(long eventId) =>
            new Specification<User>(u =>
                u.EventId == eventId && (u.UserType == UserType.Student || u.UserType == UserType.Teamlead));

        public static ISpecification<User> GetAllWithFilledProfile(long eventId) =>
            new Specification<User>(u =>
                u.EventId == eventId && (u.UserType == UserType.Student || u.UserType == UserType.Teamlead) &&
                u.ProfileFilled);


        public static ISpecification<User> GetFree(string term, long eventId) => new Specification<User>(u =>
            u.EventId == eventId
            && (u.UserType == UserType.Student || u.UserType == UserType.Teamlead)
            && u.ProfileFilled
            && (!u.TeamId.HasValue || u.Team.TeamStatus == TeamStatus.Incomplete)
            && (string.IsNullOrWhiteSpace(term)
                || (u.FirstName + " " + u.LastName).Contains(term)
                || (u.LastName + " " + u.FirstName).Contains(term)
                || u.MiddleName.Contains(term)
                || u.PhoneNumber.Contains(term)
                || u.Email.Contains(term)));


        public static ISpecification<User> GetAllStudentsInTeam(long eventId, long teemId) =>
            new Specification<User>(u => u.EventId == eventId && u.TeamId == teemId);

        public static ISpecification<User> GetByQuery(string AcademicGroup, ICollection<Competency> Competen,
            string term, long eventId) =>
            new Specification<User>(u =>
                u.EventId == eventId && u.Competencies.Any(x => x.UserCompetencyType == UserCompetencyType.Current));
    }
}