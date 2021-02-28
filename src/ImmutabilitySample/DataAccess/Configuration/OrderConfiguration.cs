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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable(nameof(Order));

            builder.Property(x => x.OrderNo).IsRequired().HasMaxLength(10);
            builder.Property(x => x.Date);
            builder.Property(x => x.GrandTotal).HasPrecision(18, 2);

            builder.OwnsOne(x => x.Customer, o =>
            {
                o.WithOwner();

                o.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
                o.Property(x => x.LastName).IsRequired().HasMaxLength(100);
                o.Property(x => x.Email).HasMaxLength(100);
            });

            builder.OwnsOne(x => x.Address, o =>
            {
                o.WithOwner();

                o.Property(x => x.Street).IsRequired().HasMaxLength(250);
                o.Property(x => x.City).IsRequired().HasMaxLength(100);
                o.Property(x => x.PostalCode).IsRequired().HasMaxLength(10);
                o.Property(x => x.Country).IsRequired().HasMaxLength(100);
            });

            builder.Metadata.FindNavigation(nameof(Order.OrderItems))
                 .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasKey(x => x.Id);
        }
    }
}
