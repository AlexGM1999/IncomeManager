using IncomeManager.Data;
using IncomeManager.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Core;

namespace IncomeManager.Services
{
    public class ExpensesServices : IExpensesServices
    {
        private readonly IncomeManagerContext _context;

        public ExpensesServices(IncomeManagerContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Expense>> GetExpenses()
        {
            return await _context.Expenses.ToListAsync();
        }


        public async Task<Expense> GetExpense(int id)
        {
            var expence = await _context.Expenses.FindAsync(id);

            if (expence == null)
            {
                throw new ObjectNotFoundException();
            }

            return expence;
        }

        public async Task<Expense> PutExpense(Expense expense)
        {
            var e = await _context.Expenses.FindAsync(expense.Id).ConfigureAwait(false);
            var user = await _context.Users.FindAsync(expense.UserId).ConfigureAwait(false);
            user.PersonalBalance += e.Amount;
            user.PersonalBalance -= expense.Amount;

            e.InvestmentId = expense.InvestmentId;
            e.UserId = expense.UserId;
            e.Amount = expense.Amount;
            e.Type = expense.Type;

            await _context.SaveChangesAsync();

            return expense;
        }

        public async Task<Expense> PostExpense(Expense expense)
        {        
            var user = await _context.Users.FindAsync(expense.UserId);
            user.PersonalBalance -= expense.Amount;

            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            return expense;
        }

        public async Task DeleteExpense(int id)
        {
            var expence = await _context.Expenses.FindAsync(id);
            if (expence == null)
            {
                throw new ObjectNotFoundException();
            }

            _context.Expenses.Remove(expence);
            await _context.SaveChangesAsync();
        }
    }
}
