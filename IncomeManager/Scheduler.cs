using Quartz;
using Quartz.Impl;

namespace IncomeManager
{
    public class Scheduler
    {
        public IScheduler? _scheduler { get; set; }
        async public void buildScheduler()
        {
            _scheduler = await SchedulerBuilder.Create()
            .UseDefaultThreadPool(x => x.MaxConcurrency = 5)
            .UsePersistentStore(x =>
            {
                // force job data map values to be considered as strings
                // prevents nasty surprises if object is accidentally serialized and then 
                // serialization format breaks, defaults to false
                x.UseProperties = true;
                x.UseClustering();
                // there are other SQL providers supported too 
                x.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=IncomeManagerContext-ce3eee62-1395-45fc-a3f6-abf50ac82105;Trusted_Connection=True;MultipleActiveResultSets=true");
                // this requires Quartz.Serialization.Json NuGet package
                x.UseJsonSerializer();
            })
            .BuildScheduler();
        }

        async public Task startScheduler()
        {
            await _scheduler.Start();
        }
    }
}
