using Microsoft.OpenApi.Models;

namespace InventoryManagement.ProductCatalog.Api.Configuration;

public class PresentationServiceInstaller : IServiceInstaller
{
    public void Install(
        IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddControllers(mvcOptions => mvcOptions
                .AddDefaultResultConvention()
                //.AddResultConvention(resultStatusMap => resultStatusMap
                //    .AddDefaultMap()
                //    .For(ResultStatus.Created, HttpStatusCode.Created)
                //    .For(ResultStatus.Ok, HttpStatusCode.OK, resultStatusOptions => resultStatusOptions
                //        .For("POST", HttpStatusCode.Created)
                //        .For("DELETE", HttpStatusCode.NoContent))
                //    .For(ResultStatus.Error, HttpStatusCode.InternalServerError))
                .UseNamespaceRouteToken());

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "DmTools Api", Version = "v1" });
            c.EnableAnnotations();
        });
    }
}
