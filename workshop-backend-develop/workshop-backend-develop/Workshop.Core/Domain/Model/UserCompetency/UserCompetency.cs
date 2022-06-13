using Workshop.Core.Domain.SharedKernel;

namespace Workshop.Core.Domain.Model.UserCompetency
{
    public class UserCompetency : Entity, IAggregateRoot, IUserCompetency
    {
        public long UserId { get; }
        public long CompetencyId { get; }
        public virtual User.User User { get; }
        public virtual Competency.Competency Competency { get; }
        public UserCompetencyType UserCompetencyType { get; }

        public UserCompetency(long userId, long competencyId, UserCompetencyType type) : base()
        {
            UserId = userId;
            CompetencyId = competencyId;
            UserCompetencyType = type;
        }

        public UserCompetency() : base()
        {
        }
    }
}