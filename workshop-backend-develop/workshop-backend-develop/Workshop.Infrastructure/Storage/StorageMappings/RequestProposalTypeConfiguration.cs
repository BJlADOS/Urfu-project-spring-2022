using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workshop.Core.Domain.Model.RequestProposal;

namespace Workshop.Infrastructure.Storage.StorageMappings
{
    public class RequestProposalTypeConfiguration : IEntityTypeConfiguration<RequestProposal>
    {
        public void Configure(EntityTypeBuilder<RequestProposal> builder)
        {
            builder.ToTable("ApplicationEssence");
            builder.HasKey(x => x.Key);
            builder.Property(x => x.Key).HasColumnName("Id");
            builder.Property(x => x.Status).HasConversion<string>();
           
        }
    }
}