using SO.Application.Cqrs;
using SO.OrderService.Domain.Order;

namespace SO.OrderService.Application.Queries.GetOrders;

public class GetOrdersQuery : IQuery<IEnumerable<OrderViewModel>>
{
}