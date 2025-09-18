using System.ComponentModel.DataAnnotations;

namespace parking_manager.Entity
{
    public class ParkingSessionsEntity
    {
        [Key, Required]

        public int Id { get; set; }
        [Required]

        public required string VehicleId { get; set; }
        [Required]

        public DateTime EntryDate { get; set; } = DateTime.Now;

        public DateTime? ExitDate { get; set; }

        public double? TotalPrice { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}