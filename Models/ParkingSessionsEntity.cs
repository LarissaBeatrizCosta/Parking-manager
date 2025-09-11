namespace parking_manager.Models
{
    public class ParkingSessionsEntity
    {
        public int Id { get; set; }
        public required int VehicleId { get; set; }

        public required string EntryDate { get; set; }
        public required string ExitDate { get; set; }

        public double TotalPrice { get; set; }

        public string CreatedAt { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public string UpdatedAt { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}