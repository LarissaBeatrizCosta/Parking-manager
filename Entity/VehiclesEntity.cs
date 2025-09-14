using System.ComponentModel.DataAnnotations;

namespace parking_manager.Entity
{
    public class VehiclesEntity
    {
        [Key, Required]
        public int Id { get; set; }

        [Required]
        public required string Plate { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}