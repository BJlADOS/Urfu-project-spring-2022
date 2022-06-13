using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workshop.Core.Domain.Model.User;

namespace Workshop.Infrastructure.Storage.StorageMappings
{
    public class UserTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.Property(x => x.UserType)
                .HasConversion<string>();

            builder.HasKey(x => x.Key);
            builder.Property(x => x.Key).HasColumnName("Id");

            builder.Property(x => x.Login).IsRequired();
            builder.HasIndex(x => new { x.Login, x.EventId }).IsUnique();

            builder.HasOne(x => x.Role).WithMany().HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}