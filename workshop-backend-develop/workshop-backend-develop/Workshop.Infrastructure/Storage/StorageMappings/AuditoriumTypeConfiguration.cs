using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workshop.Core.Domain.Model.Auditorium;
using Workshop.Core.Domain.Model.User;

namespace Workshop.Infrastructure.Storage.StorageMappings
{
    public class AuditoriumTypeConfiguration : IEntityTypeConfiguration<Auditorium>
    {
        public void Configure(EntityTypeBuilder<Auditorium> builder)
        {
            builder.ToTable("Auditoriums");

            builder.HasKey(x => x.Key);
            builder.Property(x => x.Key).HasColumnName("Id");

            builder.Property("Name").IsRequired().HasMaxLength(400).IsRequired();
        }
    }
}