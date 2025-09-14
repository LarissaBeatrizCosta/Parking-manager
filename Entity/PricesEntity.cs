using System.ComponentModel.DataAnnotations;

namespace parking_manager.Entity
{
    public class PricesEntity
    {
        [Key, Required]

        public int Id { get; set; }

        [Required]
        public required DateTime ValidFrom { get; set; }
        [Required]

        public required DateTime ValidTo { get; set; }
        [Required]


        public required double PricePerHour { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}