using MongoDB.Driver;
using SO.Application.Cqrs;
using SO.Shared.Domain.Order;

namespace SO.OrderService.Application.Queries.GetOrderSummary;

public class GetOrderSummaryQueryHandler : IQueryHandler<GetOrderSummaryQuery, IEnumerable<OrderSharedModel>>
{
    private readonly IOrderSharedRepository _orderSharedRepository;

    public GetOrderSummaryQueryHandler(IOrderSharedRepository orderSharedRepository)
    {
        _orderSharedRepository = orderSharedRepository;
    }

    public async Task<IEnumerable<OrderSharedModel>> Handle(GetOrderSummaryQuery request, CancellationToken cancellationToken)
    {
        if (!request.StartDate.HasValue && !request.EndDate.HasValue)
        {
            throw new Exception("Date rages must be filled.");
        }

        var builder = Builders<OrderSharedModel>.Filter;
        var filter = builder.Empty;
        if (request.StartDate.HasValue)
        {
            var valueDate = request.StartDate.Value.Date; // To dd/mm/yyyy 00:00:00
            filter &= builder.Gte(x => x.CreationTime, valueDate);
        }

        if (request.EndDate.HasValue)
        {
            var valueDate = request.EndDate.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59); // To dd/mm/yyyy 23:59:59
            filter &= builder.Lte(x => x.CreationTime, valueDate);
        }

        var list = await _orderSharedRepository.GetOrders(filter);
        return list;
    }
}