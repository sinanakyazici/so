using SO.Domain;

namespace SO.ProductService.Domain.Product;

public interface IProductQueryRepository : IQueryRepository
{
    Task<IEnumerable<ProductViewModel>> GetProducts();
    Task<ProductViewModel?> GetProduct(Guid productId);
}