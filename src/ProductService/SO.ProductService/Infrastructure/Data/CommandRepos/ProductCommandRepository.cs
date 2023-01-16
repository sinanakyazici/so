using Microsoft.EntityFrameworkCore;
using SO.Infrastructure.Data.EfCore;
using SO.ProductService.Domain.Product;

namespace SO.ProductService.Infrastructure.Data.CommandRepos;

public class ProductCommandRepository : EfRepository<Product>, IProductCommandRepository
{
    public ProductCommandRepository(BaseDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Product?> GetProduct(Guid id)
    {
        return await Query().SingleOrDefaultAsync(x => x.Id == id);
    }
}