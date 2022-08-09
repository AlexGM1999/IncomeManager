using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncomeManager.Models;
using IncomeManager.Services;
using Quartz;

namespace Scheduler
{
    public class ExpensesJob : IJob
    {
        private readonly ILogger<ExpensesJob> _logger;
        private readonly ExpensesServices _services;
        private readonly Expense _expense;

        public ExpensesJob(ILogger<ExpensesJob> logger,ExpensesServices services, Expense expense)
        {
            _logger = logger;
            _services = services;
            _expense = expense;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await _services.PostExpense(_expense);
            _logger.LogInformation("Success");
        }
    }
}
