using System.ComponentModel.DataAnnotations.Schema;

namespace IncomeManager.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public string? Type { get; set; } // TODO create custom type (rent, grocery...)
        public decimal Amount { get; set; }
        public DateTime DateTime { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
    }
}
