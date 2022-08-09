using Quartz;
using Scheduler;

public class Program
{
    public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddQuartz(q =>
                {
                    q.UseMicrosoftDependencyInjectionScopedJobFactory();

                    // Register the job, loading the schedule from configuration
                    q.AddJobAndTrigger<ExpensesJob>(hostContext.Configuration);
                });

                services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
            });
}