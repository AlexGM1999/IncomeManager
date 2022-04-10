namespace IncomeManager.Models
{
    public class Expenses
    {
        public int Id { get; set; }
        public string? Type { get; set; } // TODO create custom type (rent, grocery...)
        public decimal Amount { get; set; }
        public DateTime DateTime { get; set; }
    }
}
