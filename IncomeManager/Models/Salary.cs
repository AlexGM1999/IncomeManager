using System.ComponentModel.DataAnnotations.Schema;

namespace IncomeManager.Models
{
    public class Salary
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateTime { get; set; }
        public int UserId { get; set; }
    }
}
