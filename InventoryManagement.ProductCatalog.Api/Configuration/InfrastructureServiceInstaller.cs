using InventoryManagement.ProductCatalog.Api.Data;

using Microsoft.EntityFrameworkCore;

using Scrutor;

namespace InventoryManagement.ProductCatalog.Api.Configuration;

public class InfrastructureServiceInstaller : IServiceInstaller
{
    public void Install(
        IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .Scan(
                selector => selector
                    .FromAssemblies(
                        AssemblyReference.Assembly,
                        KJWT.SharedKernel.AssemblyReference.Assembly)
                    .AddClasses(false)
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsMatchingInterface()
                    .WithScopedLifetime());

        services.AddDbContext<ProductDbContext>(
            (sp, optionsBuilder) =>
            {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("Database"), default);
            });
    }
}
