using IncomeManager.Models;

namespace IncomeManager.Services
{
    public interface IExpensesServices
    {
        public Task<IEnumerable<Expense>> GetExpenses();
        public Task<Expense> GetExpense(int id);
        public Task<Expense> PutExpense(Expense expenses); 
        public Task<Expense> PostExpense(Expense expenses);
        public Task DeleteExpense(int id);
    }
}
