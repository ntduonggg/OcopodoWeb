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
    public class BlogConfiguration
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.ToTable("Blogs");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(250);

            builder.Property(x => x.Slug)
            .HasMaxLength(250);

            builder.Property(x => x.Content)
            .IsRequired();

            builder.Property(x => x.ThumbnailUrl)
            .HasMaxLength(300);

            builder.Property(x => x.Status)
            .HasDefaultValue(Status.Active);

            builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

            // ===== FK: Blog -> BlogCategory =====
            builder.HasOne(x => x.Category)
            .WithMany(x => x.Blogs)
            .HasForeignKey(x => x.CategoryId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);


            // ===== FK: Blog -> User (CreatedBy) =====
            builder.HasOne(x => x.CreatedBy)
            .WithMany()
            .HasForeignKey(x => x.CreatedById)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
