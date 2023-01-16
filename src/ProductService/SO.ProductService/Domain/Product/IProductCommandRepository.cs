using SO.Domain;

namespace SO.ProductService.Domain.Product;

public interface IProductCommandRepository : ICommandRepository<Product>
{
    Task<Product?> GetProduct(Guid id);
}