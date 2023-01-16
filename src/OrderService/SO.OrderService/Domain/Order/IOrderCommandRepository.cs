using SO.Domain;

namespace SO.OrderService.Domain.Order;

public interface IOrderCommandRepository : ICommandRepository<Order>
{
    Task<Order?> GetOrder(Guid orderId);
}