namespace IncomeManager.Models
{
    public class Investment
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public int SourceId { get; set; }
        public int UserId { get; set; }
    }
}
