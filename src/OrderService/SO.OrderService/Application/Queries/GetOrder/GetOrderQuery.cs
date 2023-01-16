using SO.Application.Cqrs;
using SO.OrderService.Domain.Order;

namespace SO.OrderService.Application.Queries.GetOrder;

public class GetOrderQuery : IQuery<OrderViewModel>
{
    public Guid OrderId { get; }

    public GetOrderQuery(Guid orderId)
    {
        OrderId = orderId;
    }
}