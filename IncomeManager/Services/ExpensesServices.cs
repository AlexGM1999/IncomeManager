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

        public async Task<Expense> PutExpense(int id, Expense expence)
        {
            _context.Entry(expence).State = EntityState.Modified;      

            await _context.SaveChangesAsync();

            return expence;
        }

        public async Task<Expense> PostExpense(Expense expence)
        {
            _context.Expenses.Add(expence);
            await _context.SaveChangesAsync();

            return expence;
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
