using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ocopodo.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocopodo.Data.Configurations
{
    internal class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            // PK
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .UseIdentityColumn();

            builder.Property(x => x.Name)
                   .HasMaxLength(100);

            builder.Property(x => x.NormalizedName)
                   .HasMaxLength(100);

            builder.Property(x => x.Description)
                   .HasMaxLength(250);

            builder.Property(x => x.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Index Identity chuẩn
            builder.HasIndex(x => x.NormalizedName)
                   .IsUnique();
        }
    }
}
