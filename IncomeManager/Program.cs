using Microsoft.EntityFrameworkCore;
using IncomeManager.Data;
using IncomeManager.Services;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


builder.Services.AddDbContext<IncomeManagerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IncomeManagerContext")), ServiceLifetime.Scoped);

// Add services to the container.
builder.Services.AddScoped<ISalaryServices, SalaryServices>();
builder.Services.AddScoped<IExpensesServices, ExpensesServices>();
builder.Services.AddScoped<IIncomeServices, IncomeServices>();
builder.Services.AddScoped<IInvestmentServices, InvestmentServices>();
builder.Services.AddScoped<IUserSevices, UserSevices>();
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

app.Run();
