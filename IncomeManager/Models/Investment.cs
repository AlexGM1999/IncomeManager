using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncomeManager.Models
{
    public class Investment
    {
        public Investment() {
            Source = new HashSet<InvestmentSource>();
        }

        public Investment(decimal balance,ICollection<InvestmentSource> source)
        {
            balance = Balance;
            source = Source;
        }

        public int Id { get; set; }
        public decimal Balance { get; set; } 
        public DateTime DateTime { get; set; }
        public List<Expense>? Expenses { get; set; }    
        public List<Income>? Income { get; set; }
        public virtual ICollection<InvestmentSource> Source { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
    }
}
