using System.ComponentModel.DataAnnotations.Schema;

namespace IncomeManager.Models
{
    public class Salary
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Date { get; set; } = DateTime.Now.ToShortDateString();
        public int UserId { get; set; }
    }
}
