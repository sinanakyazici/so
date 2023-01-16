namespace SO.Application.Cache;

public abstract class CacheRequest<TRequest> : ICacheRequest<TRequest>
{
    public virtual TimeSpan AbsoluteExpirationRelativeToNow => TimeSpan.FromMinutes(60);
    public virtual string CacheKey(TRequest request) => $"{typeof(TRequest).FullName}";
}
