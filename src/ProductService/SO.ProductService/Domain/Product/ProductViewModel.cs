namespace SO.ProductService.Domain.Product;

public class ProductViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string ProductCode { get; set; } = null!;
    public virtual DateTime CreationTime { get; set; }

    public Guid ProductTypeId { get; set; }
    public string ProductTypeName { get; set; } = null!;
    public string ProductTypeDescription { get; set; } = null!;
}