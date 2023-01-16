using System.Security.Policy;

namespace AchimDaiana_Theater.Models.LibraryViewModels
{
    public class TheaterIndexData
    {
        public IEnumerable<Theater> Theaters { get; set; }
        public IEnumerable<Play> Plays { get; set; }
        public IEnumerable<Reservation> Reservations { get; set; }
    }
}
