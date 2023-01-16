namespace SO.Application.Cache;

public class CacheConfig
{
    public string DefaultCacheType { get; set; } = nameof(CacheProviderType.InMemory);
    public RedisCacheConfig? RedisCacheConfig { get; set; } = default!;
}

public class RedisCacheConfig
{
    public string ConnectionString { get; set; } = default!;
}