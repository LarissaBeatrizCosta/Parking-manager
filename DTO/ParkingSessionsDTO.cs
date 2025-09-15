namespace parking_manager.DTO
{
    public class ParkingSessionsDTO
    {
        public required string VehiclePlate { get; set; }
        public required DateTime EntryDate { get; set; }

        public DateTime? ExitDate { get; set; }

        public double? Duration { get; set; }

        public double? HoursPaid { get; set; }

        public double? PricePerHour { get; set; }

        public double? TotalPrice { get; set; }

    }
}