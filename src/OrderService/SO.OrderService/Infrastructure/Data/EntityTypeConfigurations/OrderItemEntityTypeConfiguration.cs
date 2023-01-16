using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SO.Domain;
using SO.OrderService.Domain.Order;

namespace SO.OrderService.Infrastructure.Data.EntityTypeConfigurations
{
    public class OrderItemEntityTypeConfiguration : AuditedEntityTypeConfiguration<OrderItem, Guid>
    {
        public override void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("order_item");

            builder.Property(x => x.OrderId).HasColumnName("order_id");
            builder.Property(x => x.ProductId).HasColumnName("product_id");
            builder.Property(x => x.ProductName).HasColumnName("product_name");
            builder.Property(x => x.UnitPrice).HasColumnName("unit_price");
            builder.Property(x => x.Quantity).HasColumnName("quantity");

            base.Configure(builder);
        }
    }
}