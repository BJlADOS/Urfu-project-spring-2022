namespace Workshop.Core.Domain.Model.UserAuditorium
{
    public class UserAuditorium
    {
        public long UserId { get; set; }
        public long AuditoriumId { get; set; }
        public virtual User.User User { get; }
        public virtual Auditorium.Auditorium Auditorium { get; }
    }
}