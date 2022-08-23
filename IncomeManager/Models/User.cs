namespace IncomeManager.Models
{
    public class User
    {
        public int Id { get; set; } 
        public string Name { get; set; }    
        public decimal PersonalBalance { get; set; }
        public decimal BankBalance { get; set; }
        public decimal InvestorsBalance { get; set; }
        public decimal OtherBalance { get; set; }
    }
}
