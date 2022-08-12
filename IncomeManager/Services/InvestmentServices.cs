using IncomeManager.Data;
using IncomeManager.Models;
using System.Data.Entity.Core;
using Microsoft.EntityFrameworkCore;
using IncomeManager.DTOs;

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
            var investments = await _context.Investments.ToListAsync();
            return investments;
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

        public async Task<Investment> PutInvestment(Investment investment)
        {
            var inv = await _context.Investments.FindAsync(investment.Id).ConfigureAwait(false);
            inv.UserId = investment.UserId;
            inv.DateTime = investment.DateTime;
            inv.SourceId = investment.SourceId;
            await _context.SaveChangesAsync();

            return investment;
        }

        public async Task<Investment> PostInvestment(CreateInvestment investment)
        {
            var inv = new Investment();
            inv.SourceId = investment.SourceId;
            inv.DateTime = investment.DateTime;
            inv.UserId = investment.UserId;

            _context.Investments.Add(inv);
            await _context.SaveChangesAsync();

            return inv;
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
