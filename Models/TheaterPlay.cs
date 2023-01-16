using System.Security.Policy;

namespace AchimDaiana_Theater.Models
{
    public class TheaterPlay
    {
        public int TheaterID { get; set; }
        public int PlayID { get; set; }
        public Theater Theater { get; set; }
        public Play Play { get; set; }
    }
}
