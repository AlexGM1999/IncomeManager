namespace IncomeManager.DTOs
{
    public class CreateInvestment
    {
        public CreateInvestment(decimal amount, string source, int userId)
        {
            Amount = amount;
            Source = source;
            UserId = userId;
        }

        public decimal Amount { get; set; }

        public string Source { get; set; }

        public int UserId { get; set; }
    }
}
