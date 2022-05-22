#nullable disable
using Microsoft.EntityFrameworkCore;
using IncomeManager.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

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

            modelBuilder.Entity<Expense>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Income>()
               .HasOne<User>()
               .WithMany()
               .HasForeignKey(p => p.UserId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Investment>()
               .HasOne<User>()
               .WithMany()
               .HasForeignKey(p => p.UserId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Salary>()
               .HasOne<User>()
               .WithMany()
               .HasForeignKey(p => p.UserId)
               .OnDelete(DeleteBehavior.Cascade);

            //Investment
            modelBuilder.Entity<Expense>()
                .HasOne<Investment>()
                .WithMany()
                .HasForeignKey(p => p.InvestmentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Income>()
              .HasOne<Investment>()
              .WithMany()
              .HasForeignKey(p => p.InvestmentId)
              .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Investment>()
                .HasMany(i => i.Source)
                .WithMany(i => i.Investments);

          /*  modelBuilder.Entity<Investment>()
                .HasMany(c => c.Source).WithMany(i => i.Investments)
                .Map(t => t.MapLeftKey("Id")
                 .MapRightKey("Id")
                 .ToTable("InvestmentInvestmentSource"));*/
        }
    }
}
