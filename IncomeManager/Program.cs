using Microsoft.EntityFrameworkCore;
using IncomeManager.Data;
using IncomeManager.Services;
using System.Threading;
using Quartz;
using IncomeManager;
using Quartz.Impl;
using Quartz.Impl.Matchers;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


builder.Services.AddDbContext<IncomeManagerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IncomeManagerContext")), ServiceLifetime.Scoped);

// Add services to the container.
builder.Services.AddScoped<ISalaryServices, SalaryServices>();
builder.Services.AddScoped<IExpensesServices, ExpensesServices>();
builder.Services.AddScoped<IIncomeServices, IncomeServices>();
builder.Services.AddScoped<IInvestmentServices, InvestmentServices>();
builder.Services.AddScoped<IInvestmentSourceServices, InvestmentSourceServices>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("*")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var cronExpression = Utilities.GetDayFromDateTime(DateTime.Now.ToString("d"));
StdSchedulerFactory factory = new StdSchedulerFactory();

IScheduler scheduler = await factory.GetScheduler();
await scheduler.Start();

IJobDetail job = JobBuilder.Create<ExpensesJob>()
    .WithIdentity("expensesJob", "group1")
    .UsingJobData(new JobDataMap())// name "myJob", group "group1"
    .Build();

/*ITrigger trigger = TriggerBuilder.Create()
    .WithIdentity("expensesTrigger", "group2")
    .WithCronSchedule("0/5 * * * * ? *") //at 00:00 on day of month {cron expression} 00 00 00 {cronExpression} * ?
    .ForJob("expensesJob", "group1")
    .Build();*/

ITrigger trigger = TriggerBuilder.Create()
    .WithIdentity("expensesTrigger", "group2")
    .WithSimpleSchedule(x => x.WithIntervalInSeconds(10).RepeatForever()) //at 00:00 on day of month {cron expression} 00 00 00 {cronExpression} * ?
    .ForJob("expensesJob", "group1")
    .Build();

// Tell quartz to schedule the job using our trigger
await scheduler.ScheduleJob(job, trigger);

scheduler.ListenerManager.AddTriggerListener(new TrigListener("Trig_listener"), GroupMatcher<TriggerKey>.AnyGroup());

app.Run();
