using IncomeManager.Data;
using IncomeManager.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Core;

namespace IncomeManager.Services
{
    public class IncomeServices : IIncomeServices
    {
        private readonly IncomeManagerContext _context;

        public IncomeServices(IncomeManagerContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Income>> GetIncome()
        {
            return await _context.Income.ToListAsync();
        }

        public async Task<Income> GetIncome(int id)
        {
            var income = await _context.Income.FindAsync(id);

            if (income == null)
            {
                throw new ObjectNotFoundException();
            }

            return income;
        }

        public async Task<Income> PutIncome(Income income)
        {
            var i = await _context.Income.FindAsync(income.Id).ConfigureAwait(false);
            i.Investment = income.Investment;
            i.UserId = income.UserId;
            i.DateTime = income.DateTime;
            i.Amount = income.Amount;

            await _context.SaveChangesAsync();

            return income;
        }

        public async Task<Income> PostIncome(Income income)
        {
            var user = await _context.Users.FindAsync(income.UserId);
            user.PersonalBalance += income.Amount;

            _context.Income.Add(income);
            await _context.SaveChangesAsync();

            return income;
        }
        public async Task DeleteIncome(int id)
        {
            var income = await _context.Income.FindAsync(id);
            if (income == null)
            {
                throw new ObjectNotFoundException();
            }

            _context.Income.Remove(income);
            await _context.SaveChangesAsync();
        }
    }
}
