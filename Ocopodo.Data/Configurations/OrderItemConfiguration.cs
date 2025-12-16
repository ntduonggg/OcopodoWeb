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
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Quantity)
            .HasDefaultValue(1);

            builder.Property(x => x.Price)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(0);

            // ===== FK: OrderItem -> Order =====
            builder.HasOne(x => x.Order)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.OrderId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);


            // ===== FK: OrderItem -> Product =====
            builder.HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
