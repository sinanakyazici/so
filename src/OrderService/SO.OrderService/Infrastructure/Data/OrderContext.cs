using Microsoft.EntityFrameworkCore;
using SO.Infrastructure.Data.EfCore;
using SO.OrderService.Infrastructure.Data.EntityTypeConfigurations;

namespace SO.OrderService.Infrastructure.Data
{
    public class OrderContext : BaseDbContext
    {
        public OrderContext(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ILoggerFactory loggerFactory) : base(configuration, httpContextAccessor, loggerFactory)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemEntityTypeConfiguration());
        }
    }
}