using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workshop.Core.Domain.Model.ProjectProposal;
using Workshop.Core.Domain.Model.User;

namespace Workshop.Infrastructure.Storage.StorageMappings
{
    public class ProjectProposalTypeConfiguration : IEntityTypeConfiguration<ProjectProposal>
    {
        public void Configure(EntityTypeBuilder<ProjectProposal> builder)
        {
            builder.ToTable("ProjectProposals");
            builder.HasKey(x => x.Key);
            builder.Property(x => x.Key).HasColumnName("Id");
            builder.Property("Name").IsRequired().HasMaxLength(400).IsRequired();
            builder.Property(x => x.Status).HasConversion<string>();
            builder.HasOne(x => x.Author)
                   .WithMany()
                   .HasForeignKey(x => x.AuthorId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}