using SO.Application.Cqrs;
using SO.OrderService.Domain.Order;

namespace SO.OrderService.Application.Queries.GetOrders;

public class GetOrdersQueryHandler : IQueryHandler<GetOrdersQuery, IEnumerable<OrderViewModel>>
{
    private readonly IOrderQueryRepository _orderQueryRepository;

    public GetOrdersQueryHandler(IOrderQueryRepository orderQueryRepository)
    {
        _orderQueryRepository = orderQueryRepository;
    }

    public async Task<IEnumerable<OrderViewModel>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        return await _orderQueryRepository.GetOrders();
    }
}