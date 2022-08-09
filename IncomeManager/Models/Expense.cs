using System.ComponentModel.DataAnnotations.Schema;

namespace IncomeManager.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public string? Type { get; set; } // TODO create custom type (rent, grocery...)
        public decimal Amount { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public int  UserId { get; set; }
        public int? InvestmentId { get; set; }
        public bool Scheduled { get; set; } = false;
        public int? Date { get; set; }
    }
}
