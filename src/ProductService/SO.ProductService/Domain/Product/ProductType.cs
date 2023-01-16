using SO.Domain.Auditing;

namespace SO.ProductService.Domain.Product;

public class ProductType : AuditedAggregateRoot<Guid>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public IEnumerable<Product>? Products { get; set; }
}