namespace MvcMovie.Models
{
    public class UsageHistory
    {
        public int UsageHistoryId { get; set; }
        public int UserId { get; set; }
        public int ComputerId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal TotalCost { get; set; }

        public User? User { get; set; }
        public Computer? Computer { get; set; }
    }
}
