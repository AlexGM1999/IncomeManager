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

        public async Task<Income> PutIncome(int id, Income income)
        {
            _context.Entry(income).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return income;
        }

        public async Task<Income> PostIncome(Income income)
        {
            var user = await _context.Users.FindAsync(income.UserId);
            user.Ballance += income.Amount;

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
