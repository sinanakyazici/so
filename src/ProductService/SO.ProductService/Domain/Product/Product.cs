using SO.Domain.Auditing;

namespace SO.ProductService.Domain.Product;

public class Product : AuditedAggregateRoot<Guid>
{
    public Guid ProductTypeId { get; set; }
    public string Name { get; set; } = null!;
    public string ProductCode { get; set; } = null!;

    public ProductType ProductType { get; set; } = null!;
}