using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb1_ResturantBookingSystem.Models
{
    public class Menu
    {
        [Key]
        public int MenuId { get; set; }

        [Required]
        [MaxLength(255)]
        public string NameOfDish { get; set; }


        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

    }
}
