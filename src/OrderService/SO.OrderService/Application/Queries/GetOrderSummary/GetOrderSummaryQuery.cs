using SO.Application.Cqrs;
using SO.Shared.Domain.Order;

namespace SO.OrderService.Application.Queries.GetOrderSummary;

public class GetOrderSummaryQuery : IQuery<IEnumerable<OrderSharedModel>>
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}