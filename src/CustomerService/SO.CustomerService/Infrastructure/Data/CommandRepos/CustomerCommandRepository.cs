using Microsoft.EntityFrameworkCore;
using SO.CustomerService.Domain.Customer;
using SO.Infrastructure.Data.EfCore;

namespace SO.CustomerService.Infrastructure.Data.CommandRepos;

public class CustomerCommandRepository : EfRepository<Customer>, ICustomerCommandRepository
{
    public CustomerCommandRepository(BaseDbContext dbContext) : base(dbContext) { }

    public async Task<Customer?> GetCustomer(Guid customerId)
    {
        return await Query().Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == customerId);
    }
}