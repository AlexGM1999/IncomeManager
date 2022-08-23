namespace IncomeManager.Models
{
    public class Investment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; } 
        public string DateTime { get; set; } = "";
        public string Source { get; set; }
        public int UserId { get; set; }
    }
}
