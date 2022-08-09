using System.ComponentModel.DataAnnotations.Schema;

namespace IncomeManager.Models
{
    public class Income
    {
        public int Id { get; set; } 
        public string? Type { get; set; }
        public decimal Amount { get; set; }
        public string Date { get; set; } = DateTime.Now.ToShortDateString();
        public int UserId { get; set; }
        public int? InvestmentId { get; set; }
    }
}
