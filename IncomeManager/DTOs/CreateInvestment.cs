using IncomeManager.Models;

namespace IncomeManager.DTOs
{
    public class CreateInvestment
    {
        public CreateInvestment(decimal balance, int sourceId, int userId, DateTime dateTime)
        {
            Balance = balance;
            SourceId = sourceId;
            UserId = userId;
            DateTime = dateTime;
        }

        public decimal Balance { get; set; }

        public int SourceId { get; set; }

        public int UserId { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
