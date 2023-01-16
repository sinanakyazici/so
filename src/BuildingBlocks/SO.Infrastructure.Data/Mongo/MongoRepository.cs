using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SO.Domain;
using System.Globalization;

namespace SO.Infrastructure.Data.Mongo;

public class MongoRepository<TSharedModel> : IQueryRepository
{
    protected readonly IMongoCollection<TSharedModel> Collection;

    public MongoRepository(IMongoClient mongoClient, IOptions<MongoConfig> mongoConfig)
    {
        var database = mongoClient.GetDatabase(mongoConfig.Value.DatabaseName);
        var attr = typeof(TSharedModel).GetCustomAttributes(false).Where(x => x.GetType() == typeof(CollectionNameAttribute)).ToList();
        var collectionName = attr.Any() ? 
            ((CollectionNameAttribute)attr.First()).Value : 
            typeof(TSharedModel).Name.ToLower(CultureInfo.InvariantCulture);

        Collection = database.GetCollection<TSharedModel>(collectionName);
    }
}