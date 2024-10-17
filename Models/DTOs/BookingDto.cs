namespace Labb1_ResturantBookingSystem.Models.DTOs
{
    public class BookingDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int TableId { get; set; }

        public int NumberOfPeople { get; set; }

        public DateTime Date { get; set; }

    }

  


    

}
