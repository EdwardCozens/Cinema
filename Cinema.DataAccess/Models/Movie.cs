using System.ComponentModel.DataAnnotations;

namespace Cinema.DataAccess.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public DateOnly ReleaseDate { get; set; }

        public int Duration { get; set; }
    }
}