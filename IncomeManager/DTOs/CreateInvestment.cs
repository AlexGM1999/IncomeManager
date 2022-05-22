using IncomeManager.Models;

namespace IncomeManager.DTOs
{
    public class CreateInvestment
    {
        public CreateInvestment(decimal balance, List<int> sources, int userId, DateTime date)
        {
            Balance = balance;
            Sources = sources;
            UserId = userId;
            Date = date;
        }

        public decimal Balance { get; set; }

        public List<int> Sources { get; set; }

        public int UserId { get; set; }

        public DateTime Date { get; set; }
    }
}
