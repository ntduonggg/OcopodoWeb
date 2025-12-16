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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.CustomerName)
            .IsRequired()
            .HasMaxLength(150);

            builder.Property(x => x.CustomerPhone)
            .HasMaxLength(20);

            builder.Property(x => x.CustomerEmail)
            .HasMaxLength(150);

            builder.Property(x => x.CustomerAddress)
            .HasMaxLength(300);

            builder.Property(x => x.Status)
            .HasDefaultValue(OrderStatus.New);

            builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
