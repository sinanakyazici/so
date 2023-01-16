using SO.Domain;

namespace SO.OrderService.Domain.Order;

public interface IOrderQueryRepository : IQueryRepository
{
    Task<IEnumerable<OrderViewModel>> GetOrders();
    Task<OrderViewModel?> GetOrder(Guid orderId);
}