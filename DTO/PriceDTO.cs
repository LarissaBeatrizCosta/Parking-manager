namespace parking_manager.DTO
{
    public class PriceDTO
    {
        public required DateTime ValidFrom { get; set; }
        public required DateTime ValidTo { get; set; }
        public required double PricePerHour { get; set; }

    }
}