using System.ComponentModel.DataAnnotations;

namespace Cinema.DataAccess.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public DateOnly ReleaseDate { get; set; }

        public int Duration { get; set; }

        public int GenreId { get; set; }

        public Genre Genre { get; set; } = null!;
    }
}