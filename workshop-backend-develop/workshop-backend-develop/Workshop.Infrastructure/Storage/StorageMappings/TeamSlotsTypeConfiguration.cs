using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workshop.Core.Domain.Model.TeamSlot;

namespace Workshop.Infrastructure.Storage.StorageMappings
{
    public class TeamSlotsTypeConfiguration: IEntityTypeConfiguration<TeamSlot>
    {
        public void Configure(EntityTypeBuilder<TeamSlot> builder)
        {
            builder.ToTable("TeamSlots");
            builder.HasKey(x => x.Key);
            builder.Property(x => x.Key).HasColumnName("Id");
            builder.HasOne(x => x.Auditorium).WithMany(x => x.TeamSlots).HasForeignKey(x => x.AuditoriumId);
            builder.HasOne(x => x.Team).WithOne(x => x.TeamSlot).OnDelete(DeleteBehavior.SetNull);
        }
    }
}