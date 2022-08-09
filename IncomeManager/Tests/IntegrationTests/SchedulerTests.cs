using IncomeManager.Models;
using IncomeManager.Services;
using Xunit;
using FluentAssertions;
using IncomeManager.Controllers;

namespace IncomeManager.Tests.UnitTests
{
    public class SchedulerTests
    {
        [Fact]
        public async void Scheduler_WillAddExpense()
        {
           // var post = await _expensesServices.PostExpense(CreateExpense()); 

            //post.Scheduled.Should().Be(false);
        }

        private Expense  CreateExpense()
        {
            return new Expense
            {
                Type = "rent",
                Amount = 900,
                DateTime = DateTime.Now,    
                Scheduled = true,
                Date = 15,
                UserId = 1,
                InvestmentId = 1
            };
        }
    }
}
