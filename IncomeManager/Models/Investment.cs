using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncomeManager.Models
{
    public class Investment
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public virtual InvestmentSource? Source { get; set; }
        public int UserId { get; set; }
    }
}
