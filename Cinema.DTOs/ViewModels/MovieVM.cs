using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cinema.DTOs.ViewModels;

public class MovieVM
{
    public MovieDTO Movie { get; set; } = new MovieDTO();

    public IEnumerable<SelectListItem> Genres { get; set; } = Enumerable.Empty<SelectListItem>();
}
