namespace parking_manager.DTO
{
    public class PriceDTO
    {
        public string ValidFrom { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public string ValidTo { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public required double PricePerHour { get; set; }


    }
}