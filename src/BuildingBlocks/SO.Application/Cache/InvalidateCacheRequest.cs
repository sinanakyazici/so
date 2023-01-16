namespace SO.Application.Cache;

public abstract class InvalidateCacheRequest<TRequest> : IInvalidateCacheRequest<TRequest>
{
    // clear multiple cache
    public abstract IEnumerable<string> CacheKeys(TRequest request);
    protected string CacheKey<T>() => $"{typeof(T).FullName}";
}
