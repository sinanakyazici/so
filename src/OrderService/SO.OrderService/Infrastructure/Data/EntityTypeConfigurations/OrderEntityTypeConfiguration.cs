using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SO.Domain;
using SO.OrderService.Domain.Order;

namespace SO.OrderService.Infrastructure.Data.EntityTypeConfigurations
{
    public class OrderEntityTypeConfiguration : AuditedAggregateRootEntityTypeConfiguration<Order, Guid>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("order");

            builder.Property(x => x.CustomerId).HasColumnName("customer_id");
            builder.Property(x => x.OrderTotalPrice).HasColumnName("order_total_price");
            builder.Property(x => x.Notes).HasColumnName("notes");
            
            builder.OwnsOne(x => x.Address, nav =>
            {
                nav.Property(b => b.Country).HasColumnName("address_country");
                nav.Property(b => b.City).HasColumnName("address_city");
                nav.Property(b => b.District).HasColumnName("address_district");
                nav.Property(b => b.Text).HasColumnName("address_text");
                nav.Property(b => b.ZipCode).HasColumnName("address_zip_code");
            });

            builder.HasMany(x => x.OrderItems).WithOne(x => x.Order).HasForeignKey(x => x.OrderId).OnDelete(DeleteBehavior.Cascade);

            base.Configure(builder);
        }
    }
}