using Quartz;

namespace IncomeManager
{
	public class TrigListener : ITriggerListener
	{
        public TrigListener(string Name)
        {
            this.Name = Name;
        }

        public string Name { get; }

        public Task TriggerFired(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            Console.WriteLine("I WAS HERE");
            return Task.CompletedTask;
        }

        public Task<bool> VetoJobExecution(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            Console.WriteLine("I WAS THERE");
            return Task.FromResult(true);
        }

        public Task TriggerMisfired(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            Console.WriteLine("I WAS NOWHERE");
            return Task.CompletedTask;
        }

        public Task TriggerComplete(ITrigger trigger, IJobExecutionContext context, SchedulerInstruction triggerInstructionCode, CancellationToken cancellationToken = default)
        {
            Console.WriteLine("I WAS OVER THERE");
            return Task.CompletedTask;
        }
    }
}
