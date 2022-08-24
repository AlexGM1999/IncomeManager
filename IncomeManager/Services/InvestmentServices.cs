using IncomeManager.Data;
using IncomeManager.Models;
using IncomeManager.Constants;
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
            var user = await _context.Users.FindAsync(investment.UserId).ConfigureAwait(false);

            switch (inv.Source)
            {
                case Sources.Personal:
                    user.PersonalBalance -= inv.Amount;
                    break;
                case Sources.Investors:
                    user.InvestorsBalance -= inv.Amount;
                    break;
                case Sources.Bank:
                    user.BankBalance -= inv.Amount;
                    break;
                case Sources.Other:
                    user.OtherBalance -= inv.Amount;
                    break;
            }
            switch (investment.Source)
            {
                case Sources.Personal:
                    user.PersonalBalance += investment.Amount;
                    break;
                case Sources.Investors:
                    user.InvestorsBalance += investment.Amount;
                    break;
                case Sources.Bank:
                    user.BankBalance += investment.Amount;
                    break;
                case Sources.Other:
                    user.OtherBalance += investment.Amount;
                    break;
            }

            inv.UserId = investment.UserId;
            inv.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            inv.Source = investment.Source;
            inv.Amount = investment.Amount;

            await _context.SaveChangesAsync();

            return investment;
        }

        public async Task<Investment> PostInvestment(Investment investment)
        {
            var user = await _context.Users.FindAsync(investment.UserId).ConfigureAwait(false);

            investment.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm"); ;

            switch (investment.Source)
            {
                case Sources.Personal:
                    user.PersonalBalance += investment.Amount;
                    break;
                case Sources.Investors:
                    user.InvestorsBalance += investment.Amount;
                    break;
                case Sources.Bank:
                    user.BankBalance += investment.Amount;
                    break;
                case Sources.Other:
                    user.OtherBalance += investment.Amount;
                    break;
            }
            _context.Investments.Add(investment);
            await _context.SaveChangesAsync();

            return investment;
        }

        public async Task DeleteInvestment(int id)
        {
            var investment = await _context.Investments.FindAsync(id);
            var user = await _context.Users.FindAsync(investment.UserId).ConfigureAwait(false);
            if (investment == null)
            {
                throw new ObjectNotFoundException();
            }
            switch (investment.Source)
            {
                case Sources.Personal:
                    user.PersonalBalance -= investment.Amount;
                    break;
                case Sources.Investors:
                    user.InvestorsBalance -= investment.Amount;
                    break;
                case Sources.Bank:
                    user.BankBalance -= investment.Amount;
                    break;
                case Sources.Other:
                    user.OtherBalance -= investment.Amount;
                    break;
            }

            _context.Investments.Remove(investment);
            await _context.SaveChangesAsync();
        }
    }
}
