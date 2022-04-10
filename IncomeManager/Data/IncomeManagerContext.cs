#nullable disable
using Microsoft.EntityFrameworkCore;
using IncomeManager.Models;

namespace IncomeManager.Data
{
    public class IncomeManagerContext : DbContext
    {
        public IncomeManagerContext (DbContextOptions<IncomeManagerContext> options)
            : base(options)
        {
        }

        public DbSet<Expenses> Expenses { get; set; }

        public DbSet<Salary> Salary { get; set; }
    }
}
