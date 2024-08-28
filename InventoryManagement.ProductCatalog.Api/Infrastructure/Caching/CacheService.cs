using KJWT.SharedKernel.Caching;

using Microsoft.Extensions.Caching.Memory;

namespace InventoryManagement.ProductCatalog.Api.Infrastructure.Caching;

public sealed class CacheService : ICacheService
{
    static readonly TimeSpan DefaultExpiration = TimeSpan.FromMinutes(5);

    readonly IMemoryCache _memoryCache;

    public CacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public async Task<T> GetOrCreateAsync<T>(
        string key,
        Func<CancellationToken, Task<T>> factory,
        TimeSpan? expiration = null,
        CancellationToken cancelationToken = default)
    {
        T result = await _memoryCache.GetOrCreateAsync(
            key,
            entry =>
            {
                entry.SetAbsoluteExpiration(expiration ?? DefaultExpiration);

                return factory(cancelationToken);
            });

        return result;
    }

    public void Remove(
        string key,
        CancellationToken cancellationToken)
    {
        _memoryCache.Remove(key);
    }
}
