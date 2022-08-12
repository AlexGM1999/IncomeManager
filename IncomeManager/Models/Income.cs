using System.ComponentModel.DataAnnotations.Schema;

namespace IncomeManager.Models
{
    public class Income
    {
        public int Id { get; set; } 
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public string Investment { get; set; }
    }
}
