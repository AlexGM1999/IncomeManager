using IncomeManager.Models;

namespace IncomeManager.DTOs
{
    public class CreateInvestment
    {
        public CreateInvestment(decimal balance, InvestmentSource source, int userId, DateTime dateTime)
        {
            Balance = balance;
            Source = source;
            UserId = userId;
            DateTime = dateTime;
        }

        public decimal Balance { get; set; }

        public InvestmentSource Source { get; set; }

        public int UserId { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
