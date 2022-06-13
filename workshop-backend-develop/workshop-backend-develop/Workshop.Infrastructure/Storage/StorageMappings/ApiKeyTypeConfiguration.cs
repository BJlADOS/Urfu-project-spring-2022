using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workshop.Core.Domain.Model.ApiKey;

namespace Workshop.Infrastructure.Storage.StorageMappings
{
    public class ApiKeyTypeConfiguration : IEntityTypeConfiguration<ApiKey>
    {
        public void Configure(EntityTypeBuilder<ApiKey> builder)
        {
            builder.ToTable("ApiKeys");

            builder.Property(x => x.UserType)
                .HasConversion<string>();

            builder.HasKey(x => x.Key);
            builder.Property(x => x.Key).HasColumnName("Id");
        }
    }
}