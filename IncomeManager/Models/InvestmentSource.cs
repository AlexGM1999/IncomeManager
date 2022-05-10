using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncomeManager.Models
{
    public enum InvestmentSourceEnum
    {
        PersonalBallance = 0,
        Bank = 1,
        Investors = 2
    }
    public class InvestmentSource
    {
        public InvestmentSource()
        {
            Investments = new HashSet<Investment>();    
        }
        public virtual ICollection<Investment> Investments { get; set; }

          private InvestmentSource(InvestmentSourceEnum @enum)
          {
              Id = (int)@enum;
              Name = @enum.ToString();
          }

          [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
          public int Id { get; set; }

          [Required, MaxLength(100)]
          public string Name { get; set; }

          public static implicit operator InvestmentSource(InvestmentSourceEnum @enum) => new InvestmentSource(@enum);

          public static implicit operator InvestmentSourceEnum(InvestmentSource investmentSource) => (InvestmentSourceEnum)investmentSource.Id;
    }
}
