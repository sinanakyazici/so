using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SO.Infrastructure.Data.Mongo;
using SO.Shared.Domain.Order;

namespace SO.Shared.Infrastructure.Repo;

public class OrderSharedRepository : MongoRepository<OrderSharedModel>, IOrderSharedRepository
{
    public OrderSharedRepository(IMongoClient mongoClient, IOptions<MongoConfig> mongoConfig) : base(mongoClient, mongoConfig)
    {
    }

    public async Task<IEnumerable<OrderSharedModel>> GetOrders(FilterDefinition<OrderSharedModel> filter)
    {
        var orderSharedModels = (await Collection.FindAsync(filter)).ToList();
        return orderSharedModels;
    }

    public async Task<DeleteResult> DeleteOrders(Guid orderId)
    {
        return await Collection.DeleteManyAsync(x => x.OrderId == orderId);
    }

    public async Task<UpdateResult> UpdateOrder(FilterDefinition<OrderSharedModel> filter, UpdateDefinition<OrderSharedModel> definition, UpdateOptions? updateOptions = null)
    {
        return await Collection.UpdateOneAsync(filter, definition, updateOptions);
    }

    public async Task InsertOrder(OrderSharedModel orderSharedModel)
    {
        await Collection.InsertOneAsync(orderSharedModel);
    }
}