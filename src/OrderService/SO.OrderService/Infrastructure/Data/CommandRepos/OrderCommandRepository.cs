using Microsoft.EntityFrameworkCore;
using SO.Infrastructure.Data.EfCore;
using SO.OrderService.Domain.Order;

namespace SO.OrderService.Infrastructure.Data.CommandRepos;

public class OrderCommandRepository : EfRepository<Order>, IOrderCommandRepository
{
    public OrderCommandRepository(BaseDbContext dbContext) : base(dbContext) { }

    public async Task<Order?> GetOrder(Guid orderId)
    {
        return await Query().Include(x => x.Address).Include(x => x.OrderItems).FirstOrDefaultAsync(x => x.Id == orderId);
    }
}