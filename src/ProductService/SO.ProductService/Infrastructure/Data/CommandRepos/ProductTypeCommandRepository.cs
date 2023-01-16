using SO.Infrastructure.Data.EfCore;
using SO.ProductService.Domain.Product;

namespace SO.ProductService.Infrastructure.Data.CommandRepos;

public class ProductTypeCommandRepository : EfRepository<ProductType>
{
    public ProductTypeCommandRepository(BaseDbContext dbContext) : base(dbContext)
    {
    }
}