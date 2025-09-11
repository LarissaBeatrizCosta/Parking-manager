namespace parking_manager.Models
{
    public class PricesEntity
    {
        public int Id { get; set; }

        public required string ValidFrom { get; set; }
        public required string ValidTo { get; set; }

        public required double PricePerHour { get; set; }

        public string CreatedAt { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public string UpdatedAt { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}