using EasyCaching.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SO.Application.Cache;

namespace SO.Application.Behaviors;

public class InvalidateCachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : notnull
{
    private readonly ILogger<InvalidateCachingBehavior<TRequest, TResponse>> _logger;
    private readonly IInvalidateCacheRequest<TRequest>? _cacheRequest;
    private readonly IEasyCachingProvider _easyCachingProvider;


    public InvalidateCachingBehavior(
        ILogger<InvalidateCachingBehavior<TRequest, TResponse>> logger,
        IEasyCachingProviderFactory cachingProviderFactory,
        IOptions<CacheConfig> cacheOptions,
        IInvalidateCacheRequest<TRequest>? cacheRequest = null)
    {
        _logger = logger;
        _cacheRequest = cacheRequest;
        _easyCachingProvider = cachingProviderFactory.GetCachingProvider(cacheOptions.Value.DefaultCacheType);
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_cacheRequest == null)
        {
            // No cache
            return await next();
        }

        var cacheKeys = _cacheRequest.CacheKeys(request);
        var response = await next();

        foreach (var cacheKey in cacheKeys)
        {
            await _easyCachingProvider.RemoveAsync(cacheKey, cancellationToken);
            _logger.LogDebug("Cache data with cache key: {CacheKey} invalidated", cacheKey);
        }

        return response;
    }
}
