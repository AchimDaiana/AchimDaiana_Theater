using System.ComponentModel.DataAnnotations;

namespace AchimDaiana_Theater.Models
{
    public class Theater
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Theater Name")]
        [StringLength(50)]
        public string TheaterName { get; set; }

        [StringLength(70)]
        public string Location { get; set; }
        public ICollection<TheaterPlay>? TheaterPlays { get; set; }
    }
}
