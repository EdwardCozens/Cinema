using System.ComponentModel.DataAnnotations;

namespace Cinema.DataAccess.Models;

public class Show
{
    [Key]
    public int MovieId { get; set; }

    [Key]
    public int TheatreId { get; set; }

    public DateTime ShowTime { get; set; }

    public Movie? Movie { get; set; }

    public Theatre? Theatre { get; set; }
}
