namespace InventoryManagement.ProductCatalog.Api.Configuration;

public interface IServiceInstaller
{
    void Install(IServiceCollection services, IConfiguration configuration);
}