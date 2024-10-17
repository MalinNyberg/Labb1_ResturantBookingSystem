using System.ComponentModel.DataAnnotations;

namespace Labb1_ResturantBookingSystem.Models
{
    public class Table
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TableNumber { get; set; }

        [Required]
        public int NumberOfSeats { get; set; }

        public bool IsTableAvailable { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
