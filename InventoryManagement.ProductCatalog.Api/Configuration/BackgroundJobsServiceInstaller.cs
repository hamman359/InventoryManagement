using Quartz;

namespace InventoryManagement.ProductCatalog.Api.Configuration;

public class BackgroundJobsServiceInstaller : IServiceInstaller
{
    public void Install(
        IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IJob, Infrastructure.BackgroundJobs.ProcessOutboxMessagesJob>();

        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(Infrastructure.BackgroundJobs.ProcessOutboxMessagesJob));

            configure
                .AddJob<Infrastructure.BackgroundJobs.ProcessOutboxMessagesJob>(jobKey)
                .AddTrigger(
                    trigger =>
                        trigger.ForJob(jobKey)
                            .WithSimpleSchedule(
                                schedule =>
                                    schedule.WithIntervalInSeconds(100)
                                        .RepeatForever()));

            //configure.UseMicrosoftDependencyInjectionJobFactory();
        });

        services.AddQuartzHostedService();
    }
}
