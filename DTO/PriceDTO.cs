namespace parking_manager.DTO
{
    public class PriceDTO
    {
        public required string ValidFrom { get; set; }
        public required string ValidTo { get; set; }
        public required double PricePerHour { get; set; }

    }
}