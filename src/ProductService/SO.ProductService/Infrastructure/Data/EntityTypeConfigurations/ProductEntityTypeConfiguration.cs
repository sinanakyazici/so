using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SO.Domain;
using SO.ProductService.Domain.Product;

namespace SO.ProductService.Infrastructure.Data.EntityTypeConfigurations;

public class ProductEntityTypeConfiguration : AuditedAggregateRootEntityTypeConfiguration<Product, Guid>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("product");

        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.ProductTypeId).HasColumnName("product_type_id");
        builder.Property(x => x.ProductCode).HasColumnName("product_code");

        builder.HasOne(x => x.ProductType).WithMany(x => x.Products).HasForeignKey(x => x.ProductTypeId).OnDelete(DeleteBehavior.NoAction);

        base.Configure(builder);
    }
}