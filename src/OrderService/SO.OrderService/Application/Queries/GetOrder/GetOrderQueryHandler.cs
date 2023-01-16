using SO.Application.Cqrs;
using SO.OrderService.Domain.Order;

namespace SO.OrderService.Application.Queries.GetOrder;

public class GetOrderQueryHandler : IQueryHandler<GetOrderQuery, OrderViewModel>
{
    private readonly IOrderQueryRepository _orderQueryRepository;

    public GetOrderQueryHandler(IOrderQueryRepository orderQueryRepository)
    {
        _orderQueryRepository = orderQueryRepository;
    }

    public async Task<OrderViewModel> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var order =  await _orderQueryRepository.GetOrder(request.OrderId);
        if (order == null)
        {
            throw new Exception("Order not found");
        }

        return order;
    }
}