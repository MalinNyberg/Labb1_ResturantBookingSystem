﻿namespace Labb1_ResturantBookingSystem.Models.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class CreateCustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}
