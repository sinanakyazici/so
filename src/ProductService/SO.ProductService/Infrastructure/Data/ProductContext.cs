using Microsoft.EntityFrameworkCore;
using SO.Infrastructure.Data.EfCore;
using SO.ProductService.Infrastructure.Data.EntityTypeConfigurations;

namespace SO.ProductService.Infrastructure.Data
{
    public class ProductContext : BaseDbContext
    {
        public ProductContext(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ILoggerFactory loggerFactory) : base(configuration, httpContextAccessor, loggerFactory)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductTypeEntityTypeConfiguration());
        }
    }
}