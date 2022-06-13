using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workshop.Core.Domain.Model.Role;

namespace Workshop.Infrastructure.Storage.StorageMappings
{
    public class RoleTypeConfiguration: IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            
            builder.HasKey(x => x.Key);
            builder.Property(x => x.Key).HasColumnName("Id");

            builder.HasOne(x => x.Project).WithMany(x => x.Roles).HasForeignKey(x => x.ProjectId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}