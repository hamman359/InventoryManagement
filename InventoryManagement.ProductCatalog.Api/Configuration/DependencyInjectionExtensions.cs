using System.Reflection;

using FluentValidation;

namespace InventoryManagement.ProductCatalog.Api.Configuration;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection InstallServices(
        this IServiceCollection services,
        IConfiguration configuration,
        params Assembly[] assemblies)
    {
        IEnumerable<IServiceInstaller> serviceInstallers = assemblies
            .SelectMany(a => a.DefinedTypes)
            .Where(IsAssignableToType<IServiceInstaller>)
            .Select(Activator.CreateInstance)
            .Cast<IServiceInstaller>();

        foreach(IServiceInstaller serviceInstaller in serviceInstallers)
        {
            serviceInstaller.Install(services, configuration);
        }

        static bool IsAssignableToType<T>(TypeInfo typeinfo) =>
            typeof(T).IsAssignableFrom(typeinfo) &&
            !typeinfo.IsInterface &&
            !typeinfo.IsAbstract;

        return services;
    }
}