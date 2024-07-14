namespace Cinema.DataAccess.Models;

public class Theatre
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<Show> Shows { get; set; } = new List<Show>();
}
