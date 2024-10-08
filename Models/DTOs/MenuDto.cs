namespace Labb1_ResturantBookingSystem.Models.DTOs
{
    public class MenuDto
    {
        public int MenuId { get; set; }
        public string NameOfDish { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; } // Admin can see and manage availability
    }

}
