namespace parking_manager.DTO
{
    public class ParkingSessionsDTO
    {
        public required string VehiclePlate { get; set; }
        public string EntryDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public string? ExitDate { get; set; }
        public string? Duration { get; set; } 
        public double HoursPaid { get; set; } 
        public double PricePerHour { get; set; }
        public double TotalPrice { get; set; }

    }
}