using IncomeManager.Data;
using IncomeManager.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Core;

namespace IncomeManager.Services
{
    public class SalaryServices : ISalaryServices
    {
        private readonly IncomeManagerContext _context;

        public SalaryServices(IncomeManagerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Salary>> GetSalary()
        {
            return await _context.Salary.ToListAsync();
        }

        public async Task<Salary> GetSalary(int id)
        {
            var salary = await _context.Salary.FindAsync(id);

            if (salary == null)
            {
                throw new ObjectNotFoundException();
            }

            return salary;
        }

        public async Task<Salary> PutSalary(Salary salary)
        {

            var s = await _context.Salary.FindAsync(salary.Id).ConfigureAwait(false);
            var user = await _context.Users.FindAsync(salary.UserId).ConfigureAwait(false);
            user.PersonalBalance -= s.Amount;
            user.PersonalBalance += salary.Amount;

            s.Amount = salary.Amount;
            s.UserId = salary.UserId;
            s.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            await _context.SaveChangesAsync().ConfigureAwait(false);

            return salary;
        }

        public async Task<Salary> PostSalary(Salary salary)
        {
            salary.DateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            var user = await _context.Users.FindAsync(salary.UserId);
            user.PersonalBalance += salary.Amount;

            _context.Salary.Add(salary);
            await _context.SaveChangesAsync();

            return salary;
        }

        public async Task DeleteSalary(int id)
        {
            var salary = await _context.Salary.FindAsync(id);
            if (salary == null)
            {
                throw new ObjectNotFoundException();
            }

            var user = await _context.Users.FindAsync(salary.UserId);
            user.PersonalBalance -= salary.Amount;

            _context.Salary.Remove(salary);
            await _context.SaveChangesAsync();
        }
    }
}