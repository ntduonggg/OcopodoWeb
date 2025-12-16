using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ocopodo.Data.Entities;
using Ocopodo.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocopodo.Data.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            // PK
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .UseIdentityColumn();

            // Identity fields
            builder.Property(x => x.UserName)
                   .HasMaxLength(100);

            builder.Property(x => x.NormalizedUserName)
                   .HasMaxLength(100);

            builder.Property(x => x.Email)
                   .HasMaxLength(150);

            builder.Property(x => x.NormalizedEmail)
                   .HasMaxLength(150);

            // Custom fields
            builder.Property(x => x.FullName)
                   .HasMaxLength(150);

            builder.Property(x => x.Status)
                   .HasDefaultValue(Status.Active);

            builder.Property(x => x.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Index (Identity chuẩn)
            builder.HasIndex(x => x.NormalizedUserName)
                   .IsUnique();

            builder.HasIndex(x => x.NormalizedEmail);
        }
    }
}
