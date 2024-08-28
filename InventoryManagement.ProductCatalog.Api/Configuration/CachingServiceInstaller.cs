using InventoryManagement.ProductCatalog.Api.Infrastructure.Caching;

using KJWT.SharedKernel.Caching;

namespace InventoryManagement.ProductCatalog.Api.Configuration;

public class CachingServiceInstaller : IServiceInstaller
{
    public void Install(
        IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMemoryCache();

        services.AddSingleton<ICacheService, CacheService>();
    }
}
