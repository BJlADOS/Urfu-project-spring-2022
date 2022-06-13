using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workshop.Core.Domain.Model.UserAuditorium;

namespace Workshop.Infrastructure.Storage.StorageMappings
{
    public class UserAuditoriumTypeConfiguration : IEntityTypeConfiguration<UserAuditorium>
    {
        public void Configure(EntityTypeBuilder<UserAuditorium> builder)
        {
            builder.ToTable("UserAuditoriums");
            builder.HasKey(x => new { x.UserId, x.AuditoriumId });

            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.AuditoriumId).IsRequired();

            builder.HasOne(x => x.User).WithMany(x => x.Auditoriums).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Auditorium).WithMany(x => x.Experts).HasForeignKey(x => x.AuditoriumId).OnDelete(DeleteBehavior.Cascade);
        }
    }

}
