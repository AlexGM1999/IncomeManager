using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using IncomeManager.Data;
using IncomeManager.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<IncomeManagerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IncomeManagerContext")));

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
