﻿using System.ComponentModel.DataAnnotations;

namespace Labb1_ResturantBookingSystem.Models.DTOs
{
    public class TableDto
    {
        public int TableId { get; set; }
        public int NumberOfSeats { get; set; }
        public int TableNumber { get; set; }
    }

    public class CreateTableDto
    {
        public int NumberOfSeats { get; set; }
        public int TableNumber { get; set; }
    }

    public class TableAvailabilityRequestDto
    {
        public int TableId { get; set;}
        public DateTime BookingTime { get; set; }    
    
    }

    public class TableAvailabilityResponseDto
    {
        public int TableId { get; set; }
        public bool IsTableAvailable { get; set; }
    }


}
