namespace SO.Application.Cache;

public interface IInvalidateCacheRequest<in TRequest>
{
    IEnumerable<string> CacheKeys(TRequest request);
}