using System.ComponentModel.DataAnnotations;

namespace Labb1_ResturantBookingSystem.Models
{
    public class Table
    {
        [Key]
        public int TableId { get; set; }

        [Required]
        public int TableNumber { get; set; }

        [Required]
        public int NumberOfSeats { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
