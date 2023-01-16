using EasyCaching.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SO.Application.Cache;

namespace SO.Application.Behaviors;

public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : notnull
{
    private readonly ILogger<CachingBehavior<TRequest, TResponse>> _logger;
    private readonly ICacheRequest<TRequest>? _cacheRequest;
    private readonly IEasyCachingProvider _easyCachingProvider;

    public CachingBehavior(
        ILogger<CachingBehavior<TRequest, TResponse>> logger,
        IEasyCachingProviderFactory cachingProviderFactory,
        IOptions<CacheConfig> cacheOptions,
        ICacheRequest<TRequest>? cacheRequest = null)
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

        var cacheKey = _cacheRequest.CacheKey(request);
        var cachedResponse = await _easyCachingProvider.GetAsync<TResponse>(cacheKey, cancellationToken);

        if (cachedResponse.Value != null)
        {
            _logger.LogDebug("Response retrieved {TRequest} from cache. CacheKey: {CacheKey}", typeof(TRequest).FullName, cacheKey);
            // Do not continue pipeline. 
            return cachedResponse.Value;
        }

        var response = await next();

        await _easyCachingProvider.SetAsync(cacheKey, response, _cacheRequest.AbsoluteExpirationRelativeToNow, cancellationToken);

        _logger.LogDebug("Caching response for {TRequest} with cache key: {CacheKey}", typeof(TRequest).FullName, cacheKey);
        return response;
    }
}