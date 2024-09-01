namespace Labb1_ResturantBookingSystem.Models.DTOs
{
    public class MenuDto
    {
        public int MenuId { get; set; }
        public string NameOfDish { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; } // Admin can see and manage availability
    }

    public class CreateMenuDto
    {
        public string NameOfDish { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
    }

    public class UpdateMenuDto
    {  
        public string NameOfDish { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; } // Admin can set availability status
    }

}
