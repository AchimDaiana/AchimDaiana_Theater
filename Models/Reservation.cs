namespace AchimDaiana_Theater.Models
{
    public class Reservation
    {
        public int ReservationID { get; set; }
        public int CustomerID { get; set; }
        public int PlayID { get; set; }

        public DateTime ReservationDate { get; set; }
        public Customer? Customer { get; set; }
        public Play? Play { get; set; }
    }
}
