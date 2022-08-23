using IncomeManager.Data;
using IncomeManager.Models;
using IncomeManager.Constants;
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

            switch (e.Investment)
            {
                case Sources.Personal:
                    user.PersonalBalance += e.Amount;
                    break;
                case Sources.Investors:
                    user.InvestorsBalance += e.Amount;
                    break;
                case Sources.Bank:
                    user.BankBalance += e.Amount;
                    break;
                case Sources.Other:
                    user.OtherBalance += e.Amount;
                    break;
            }
            switch (expense.Investment)
            {
                case Sources.Personal:
                    user.PersonalBalance -= expense.Amount;
                    break;
                case Sources.Investors:
                    user.InvestorsBalance -= expense.Amount;
                    break;
                case Sources.Bank:
                    user.BankBalance -= expense.Amount;
                    break;
                case Sources.Other:
                    user.OtherBalance -= expense.Amount;
                    break;
            }

            e.Investment = expense.Investment;
            e.UserId = expense.UserId;
            e.Amount = expense.Amount;
            e.Type = expense.Type;
            e.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            await _context.SaveChangesAsync();

            return expense;
        }

        public async Task<Expense> PostExpense(Expense expense)
        {
            var user = await _context.Users.FindAsync(expense.UserId);
            switch (expense.Investment)
            {
                case Sources.Personal:
                    user.PersonalBalance -= expense.Amount;
                    break;
                case Sources.Investors:
                    user.InvestorsBalance -= expense.Amount;
                    break;
                case Sources.Bank:
                    user.BankBalance -= expense.Amount;
                    break;
                case Sources.Other:
                    user.OtherBalance -= expense.Amount;
                    break;
            }
            expense.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            return expense;
        }

        public async Task DeleteExpense(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            var user = await _context.Users.FindAsync(expense.UserId).ConfigureAwait(false);

            if (expense == null)
            {
                throw new ObjectNotFoundException();
            }
            switch (expense.Investment)
            {
                case Sources.Personal:
                    user.PersonalBalance += expense.Amount;
                    break;
                case Sources.Investors:
                    user.InvestorsBalance += expense.Amount;
                    break;
                case Sources.Bank:
                    user.BankBalance += expense.Amount;
                    break;
                case Sources.Other:
                    user.OtherBalance += expense.Amount;
                    break;
            }
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
        }
    }
}
