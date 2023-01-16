using MongoDB.Driver;
using SO.Domain;

namespace SO.Shared.Domain.Order;

public interface IOrderSharedRepository : ISharedRepository
{
    Task InsertOrder(OrderSharedModel orderSharedModel);
    Task<IEnumerable<OrderSharedModel>> GetOrders(FilterDefinition<OrderSharedModel> filter);
    Task<DeleteResult> DeleteOrders(Guid orderId);
    Task<UpdateResult> UpdateOrder(FilterDefinition<OrderSharedModel> filter, UpdateDefinition<OrderSharedModel> definition, UpdateOptions? updateOptions = null);
}