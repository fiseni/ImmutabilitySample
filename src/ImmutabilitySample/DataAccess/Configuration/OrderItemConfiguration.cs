using ImmutabilitySample.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmutabilitySample.DataAccess.Configuration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable(nameof(OrderItem));

            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Price).HasPrecision(18, 2);

            // Even though we're not configuring anything specific for OrderId property,
            // it's crucial to write the following line, and let the EF know that we want to include this property.
            // EF by default, excludes all properties with getters only. It assumes they are calculated properties.
            builder.Property(x => x.OrderId);

            builder.HasKey(x => x.Id);
        }
    }
}
