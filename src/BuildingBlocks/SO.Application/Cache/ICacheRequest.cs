namespace SO.Application.Cache;

public interface ICacheRequest<in TRequest>
{
    TimeSpan AbsoluteExpirationRelativeToNow { get; }
    string CacheKey(TRequest request);
}