namespace MvcMovie.Models
{
    public class RevenueReportViewModel
    {
        public int ComputerId { get; set; }
        public string ComputerName { get; set; }
        public decimal TotalRevenue { get; set; }
        public TimeSpan TotalUsageTime { get; set; }
    }
}
