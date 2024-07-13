using System.ComponentModel.DataAnnotations;

namespace Cinema.DTOs
{
    public class MovieDTO
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public DateOnly ReleaseDate { get; set; }

        public int Duration { get; set; }
    }
}