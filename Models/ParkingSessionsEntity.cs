using System.ComponentModel.DataAnnotations;

namespace parking_manager.Models
{
    public class ParkingSessionsEntity
    {
        [Key, Required]

        public int Id { get; set; }
        [Required]

        public required string VehicleId { get; set; }
        [Required]

        public required string EntryDate { get; set; }
        [Required]

        public required string ExitDate { get; set; }
        [Required]

        public double TotalPrice { get; set; }

        public string CreatedAt { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        public string UpdatedAt { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}