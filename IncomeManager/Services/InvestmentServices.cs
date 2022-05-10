using IncomeManager.Data;
using IncomeManager.Models;
using System.Data.Entity.Core;
using Microsoft.EntityFrameworkCore;

namespace IncomeManager.Services
{
    public class InvestmentServices : IInvestmentServices
    {
        private readonly IncomeManagerContext _context;

        public InvestmentServices(IncomeManagerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Investment>> GetInvestments()
        {
            return await _context.Investments.ToListAsync();
        }

        public async Task<Investment> GetInvestment(int id)
        {
            var investment = await _context.Investments.FindAsync(id);

            if (investment == null)
            {
                throw new ObjectNotFoundException();
            }

            return investment;
        }

        public async Task<Investment> PutInvestment(int id, Investment investment)
        {
            _context.Entry(investment).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return investment;
        }

        public async Task<Investment> PostInvestment(Investment investment)
        {
            _context.Investments.Add(investment);
            await _context.SaveChangesAsync();

            return investment;
        }

        public async Task DeleteInvestment(int id)
        {
            var investment = await _context.Investments.FindAsync(id);
            if (investment == null)
            {
                throw new ObjectNotFoundException();
            }

            _context.Investments.Remove(investment);
            await _context.SaveChangesAsync();
        }
    }
}
