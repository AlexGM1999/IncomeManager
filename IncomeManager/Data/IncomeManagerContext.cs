#nullable disable
using Microsoft.EntityFrameworkCore;
using IncomeManager.Models;

namespace IncomeManager.Data
{
    public class IncomeManagerContext : DbContext
    {
        public IncomeManagerContext(DbContextOptions<IncomeManagerContext> options)
            : base(options)
        {
        }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Salary> Salary { get; set; }
        public DbSet<Investment> Investments { get; set; }
        public DbSet<Income> Income { get; set; }
        public DbSet<InvestmentSource> InvestmentSources { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
