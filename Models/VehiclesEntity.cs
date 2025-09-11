namespace parking_manager.Models
{
    public class VehiclesEntity
    {
        public int Id { get; set; }
        public required string Plate { get; set; }

        public string CreatedAt { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public string UpdatedAt { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        
    }
}