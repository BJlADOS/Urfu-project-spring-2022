namespace Workshop.Core.Domain.Model.Role
{
    public interface IRole
    {
        long Key { get; }
        string Name { get; }
        long ProjectId { get; }
    }
}