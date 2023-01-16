namespace SO.Infrastructure.Data.Mongo;

public class MongoConfig
{
    public MongoConfig(string connectionString, string databaseName)
    {
        ConnectionString = connectionString;
        DatabaseName = databaseName;
    }

    public MongoConfig()
    {

    }

    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
}