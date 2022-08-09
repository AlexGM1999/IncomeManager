using IncomeManager.Data;
using IncomeManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity.Core;

namespace IncomeManager.Services
{
    public class InvestmentSourceServices : IInvestmentSourceServices
    {
        private readonly IncomeManagerContext _context;

        public InvestmentSourceServices(IncomeManagerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InvestmentSource>> GetInvestmentSources()
        {
            return await _context.InvestmentSources.ToListAsync();
        }

        public async Task<InvestmentSource> GetInvestmentSource(int id)
        {
            var investmentSource = await _context.InvestmentSources.FindAsync(id);

            if (investmentSource == null)
            {
                throw new ObjectNotFoundException();
            }

            return investmentSource;
        }

        public async Task<InvestmentSource> PutInvestmentSource( InvestmentSource investmentSource)
        {
            var source = await _context.InvestmentSources.FindAsync(investmentSource.Id).ConfigureAwait(false);
            //todo

            await _context.SaveChangesAsync(); 
      
            return investmentSource;
        }

        public async Task<InvestmentSource> PostInvestmentSource(InvestmentSource investmentSource)
        {
            _context.InvestmentSources.Add(investmentSource);
             await _context.SaveChangesAsync();

            return investmentSource; 
        }

        public async Task DeleteInvestmentSource(int id)
        {
            var investmentSource = await _context.InvestmentSources.FindAsync(id);
            if (investmentSource == null)
            {
                throw new ObjectNotFoundException();
            }

            _context.InvestmentSources.Remove(investmentSource);
            await _context.SaveChangesAsync();
        }
    }
}
