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

        public async Task<Salary> PutSalary(int id, Salary salary)
        {
            var s = _context.Salary.Find(id);
            var user = await _context.Users.FindAsync(salary.UserId);
            user.Ballance -= s.Amount;
            user.Ballance += salary.Amount;

            _context.Entry(salary).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return salary;
        }

        public async Task<Salary> PostSalary(Salary salary)
        {
            var user = await _context.Users.FindAsync(salary.UserId);
            user.Ballance += salary.Amount;

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

            _context.Salary.Remove(salary);
            await _context.SaveChangesAsync();
        }
    }
}