using System.ComponentModel.DataAnnotations;

namespace Labb1_ResturantBookingSystem.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
