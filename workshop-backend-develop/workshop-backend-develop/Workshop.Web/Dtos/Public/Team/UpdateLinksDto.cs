namespace Workshop.Web.Dtos.Public.Team
{
    public class UpdateLinksDto
    {
        public long TeamId { get; set; }
        public string PMSLink { get; set; }
        public string RepositoryLink { get; set; }
        public string AdditionalLink { get; set; }
    }
}