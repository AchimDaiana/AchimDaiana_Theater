using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace AchimDaiana_Theater.Models
{
    public class Play
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }

        public int? WriterID { get; set; }
        public Writer? Writer { get; set; }

        public ICollection<Reservation>? Reservations { get; set; }
        public ICollection<TheaterPlay>? TheaterPlays{ get; set; }

    }
}
