using Workshop.Core.Domain.Model.User;

namespace Workshop.Web.Dtos.TeamLead
{
    public class RequestProposalDto
    {
        public User UserFrom { get; set; }
        public User UserTo { get; set; }
        public long Id { get; set; }

    }
}
