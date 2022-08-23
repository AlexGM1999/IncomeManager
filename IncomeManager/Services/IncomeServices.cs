using IncomeManager.Data;
using IncomeManager.Models;
using IncomeManager.Constants;
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
            var user = await _context.Users.FindAsync(income.UserId).ConfigureAwait(false);

            switch (i.Investment)
            {
                case Sources.Personal:
                    user.PersonalBalance -= i.Amount;
                    break;
                case Sources.Investors:
                    user.InvestorsBalance -= i.Amount;
                    break;
                case Sources.Bank:
                    user.BankBalance -= i.Amount;
                    break;
                case Sources.Other:
                    user.OtherBalance -= i.Amount;
                    break;
            }
            switch (income.Investment)
            {
                case Sources.Personal:
                    user.PersonalBalance += income.Amount;
                    break;
                case Sources.Investors:
                    user.InvestorsBalance += income.Amount;
                    break;
                case Sources.Bank:
                    user.BankBalance += income.Amount;
                    break;
                case Sources.Other:
                    user.OtherBalance += income.Amount;
                    break;
            }

            i.Investment = income.Investment;
            i.UserId = income.UserId;
            i.DateTime = income.DateTime;
            i.Amount = income.Amount;
            i.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            await _context.SaveChangesAsync();

            return income;
        }

        public async Task<Income> PostIncome(Income income)
        {
            var user = await _context.Users.FindAsync(income.UserId);
            switch (income.Type)
            {
                case Sources.Personal:
                    user.PersonalBalance += income.Amount;
                    break;
                case Sources.Investors:
                    user.InvestorsBalance += income.Amount;
                    break;
                case Sources.Bank:
                    user.BankBalance += income.Amount;
                    break;
                case Sources.Other:
                    user.OtherBalance += income.Amount;
                    break;
            }
            income.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            _context.Income.Add(income);
            await _context.SaveChangesAsync();

            return income;
        }
        public async Task DeleteIncome(int id)
        {
            var income = await _context.Income.FindAsync(id);
            var user = await _context.Users.FindAsync(income.UserId);
            if (income == null)
            {
                throw new ObjectNotFoundException();
            }

            switch (income.Type)
            {
                case Sources.Personal:
                    user.PersonalBalance -= income.Amount;
                    break;
                case Sources.Investors:
                    user.InvestorsBalance -= income.Amount;
                    break;
                case Sources.Bank:
                    user.BankBalance -= income.Amount;
                    break;
                case Sources.Other:
                    user.OtherBalance -= income.Amount;
                    break;
            }

            _context.Income.Remove(income);
            await _context.SaveChangesAsync();
        }
    }
}
