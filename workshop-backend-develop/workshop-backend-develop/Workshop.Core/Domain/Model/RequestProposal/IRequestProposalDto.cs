namespace Workshop.Core.Domain.Model.RequestProposal
{
    public interface IRequestProposalDto
    {
        long UserId { get; set; }
        long TeamleadId { get; set; }
        long ProjectId { get; set; }
        RequestStatus Status { get; set; }
        string RoleName { get; set; }
    }
}