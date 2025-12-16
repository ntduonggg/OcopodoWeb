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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

            builder.Property(x => x.Slug)
            .HasMaxLength(200);

            builder.Property(x => x.Description)
            .HasMaxLength(2000);

            builder.Property(x => x.Price)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0);

            builder.Property(x => x.MainImageUrl)
            .HasMaxLength(300);


            builder.Property(x => x.Status)
            .HasDefaultValue(Status.Active);

            builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

            // ===== FK: Product -> ProductCategory =====
            builder.HasOne(x => x.Category)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.CategoryId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.Slug);
        }
    }
}
