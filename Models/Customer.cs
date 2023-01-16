﻿namespace AchimDaiana_Theater.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public ICollection<Reservation>? Reservations { get; set; }
    }
}
