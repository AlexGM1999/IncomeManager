using Microsoft.EntityFrameworkCore;
using IncomeManager.Models;

namespace IncomeManager.Data
{
    public class IncomeManagerContext : DbContext
    {
        public IncomeManagerContext(DbContextOptions<IncomeManagerContext> options)
            : base(options)
        { }

        public DbSet<Expense>? Expenses { get; set; }
        public DbSet<Salary>? Salary { get; set; }
        public DbSet<Investment>? Investments { get; set; }
        public DbSet<Income>? Income { get; set; }
        public DbSet<User>? Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Expense>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Income>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Investment>()
               .HasOne<User>()
               .WithMany()
               .HasForeignKey(p => p.UserId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Salary>()
               .HasOne<User>()
               .WithMany()
               .HasForeignKey(p => p.UserId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
