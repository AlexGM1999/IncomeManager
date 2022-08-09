using IncomeManager.Services;
using Quartz;
using IncomeManager;

namespace IncomeManager
{
    public class ExpensesJob : IJob
    {
        private readonly IExpensesServices _expense;
        public ExpensesJob(IExpensesServices expense)
        {
            _expense = expense;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("Hello worlds");
            var list = from expense in await _expense.GetExpenses()
                       where expense.Date == Utilities.GetDayFromDateTime(DateTime.Now.ToString("d"))
                       select expense;

            foreach (var item in list)
            {
                await _expense.PostExpense(item);
            }

        }
    }
}
