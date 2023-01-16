using Microsoft.EntityFrameworkCore;
using SO.CustomerService.Infrastructure.Data.EntityTypeConfigurations;
using SO.Infrastructure.Data.EfCore;

namespace SO.CustomerService.Infrastructure.Data;

public class CustomerContext : BaseDbContext
{
    public CustomerContext(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ILoggerFactory loggerFactory) : base(configuration, httpContextAccessor, loggerFactory)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CustomerEntityTypeConfiguration());
    }
}