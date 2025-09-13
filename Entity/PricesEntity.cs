using System.ComponentModel.DataAnnotations;

namespace parking_manager.Entity
{
    public class PricesEntity
    {
        [Key, Required]

        public int Id { get; set; }

        [Required]
        public required string ValidFrom { get; set; }
        [Required]

        public required string ValidTo { get; set; }
        [Required]


        public required double PricePerHour { get; set; }


        public string CreatedAt { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        public string UpdatedAt { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}