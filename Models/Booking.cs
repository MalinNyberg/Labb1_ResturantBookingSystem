using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb1_ResturantBookingSystem.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int NumberOfPeople { get; set; }

    }
}
