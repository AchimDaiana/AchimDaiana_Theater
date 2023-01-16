namespace AchimDaiana_Theater.Models
{
    public class Writer
    {
        public int WriterID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        

        public ICollection<Play>? Plays { get; set; }
    }
}
