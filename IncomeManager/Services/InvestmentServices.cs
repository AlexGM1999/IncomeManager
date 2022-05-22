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
            foreach (var item in investments)
            {
                var query = _context.Investments.Where(inv => inv.Id==item.Id).SelectMany(inv => inv.Source).ToList();
                item.Source = query;    
            }
            return investments;
        }

        public async Task<Investment> GetInvestment(int id)
        {
            var query = _context.Investments.Where(inv => inv.Id == id).SelectMany(inv => inv.Source).ToList();
            var investment = await _context.Investments.FindAsync(id);

            investment.Source = query;
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

        public async Task<Investment> PostInvestment(CreateInvestment investment)
        {
            var sources = _context.InvestmentSources.Where(a => investment.Sources.Contains(a.Id)).ToList();
            var inv = new Investment();
            inv.Balance = investment.Balance;
            inv.Source = sources;
            inv.DateTime = investment.Date;
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
