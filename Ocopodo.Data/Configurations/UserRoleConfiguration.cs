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
    internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRoles");

            // FK → User
            builder.HasOne(x => x.User)
                   .WithMany(x => x.UserRoles)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // FK → Role
            builder.HasOne(x => x.Role)
                   .WithMany(x => x.UserRoles)
                   .HasForeignKey(x => x.RoleId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Index cho quick join
            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.RoleId);
        }
    }
}
