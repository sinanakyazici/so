using EasyCaching.Redis;
using EasyCaching.Serialization.SystemTextJson.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace SO.Application.Cache;

public static class CacheExtension
{
    public static IServiceCollection AddCustomCaching(this IServiceCollection services, CacheConfig cacheConfig)
    {
        services.AddEasyCaching(option =>
        {
            if (cacheConfig.RedisCacheConfig is not null && cacheConfig.DefaultCacheType == nameof(CacheProviderType.Redis))
            {
                option.UseRedis(
                    config =>
                    {
                        config.DBConfig = new RedisDBOptions { Configuration = cacheConfig.RedisCacheConfig.ConnectionString };
                        config.SerializerName = "json";
                    },
                    nameof(CacheProviderType.Redis)).WithSystemTextJson("json");
            }
            else if (cacheConfig.DefaultCacheType == nameof(CacheProviderType.InMemory))
            {
                option.UseInMemory(nameof(CacheProviderType.InMemory));
            }
        });

        return services;
    }
}