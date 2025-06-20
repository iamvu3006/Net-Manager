namespace MvcMovie.Models
{
    public class Computer
    {
        public int ComputerId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; } = "Available"; // Available, In Use, Maintenance, ...
        public bool IsInUse { get; set; } = false;

        public decimal PricePerHour { get; set; }

        public ICollection<UsageHistory>? UsageHistories { get; set; }
    }
}
