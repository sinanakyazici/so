using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SO.Domain;
using SO.ProductService.Domain.Product;

namespace SO.ProductService.Infrastructure.Data.EntityTypeConfigurations;

public class ProductTypeEntityTypeConfiguration : AuditedAggregateRootEntityTypeConfiguration<ProductType, Guid>
{
    public override void Configure(EntityTypeBuilder<ProductType> builder)
    {
        builder.ToTable("product_type");

        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Description).HasColumnName("description");

        builder.HasMany(x => x.Products).WithOne(x => x.ProductType).HasForeignKey(x => x.ProductTypeId).OnDelete(DeleteBehavior.NoAction);

        base.Configure(builder);
    }
}