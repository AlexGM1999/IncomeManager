using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncomeManager.Models
{
    public enum InvestmentSourceEnum
    {
        PersonalBalance = 1,
        Bank = 2,
        Investors = 3,
        Other  = 4
    }
    public class InvestmentSource
    {
        public virtual ICollection<Investment>? Investments { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
