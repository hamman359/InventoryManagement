using FluentValidation;

using KJWT.SharedKernel.Behaviors;

namespace InventoryManagement.ProductCatalog.Api.Configuration;

public class ApplicationServiceInstaller : IServiceInstaller
{
    public void Install(
        IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(AssemblyReference.Assembly);

            cfg.AddOpenBehavior(typeof(QueryCachingPipelineBehavior<,>));
        });

        //services.Decorate(typeof(INotificationHandler<>), typeof(IdempotentDomainEventHandler<>));

        services.AddValidatorsFromAssembly(
            AssemblyReference.Assembly,
            includeInternalTypes: true);

        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(LoggingPipelineBehavior<,>));
    }
}
