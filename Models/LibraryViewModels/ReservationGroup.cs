using System.ComponentModel.DataAnnotations;

namespace AchimDaiana_Theater.Models.LibraryViewModels
{
    public class ReservationGroup
    {
        [DataType(DataType.Date)]
        public DateTime? ReservedDate { get; set; }
        public int PlayCount { get; set; }
    }
}
