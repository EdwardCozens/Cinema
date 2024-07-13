using Cinema.DataAccess.Data;
using Cinema.DataAccess.Models;
using Cinema.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly ApplicationDbContext context;

    public MoviesController(ApplicationDbContext context)
    {
        this.context = context;
    }

    [HttpGet(Name = "Get_Movies")]
    public async Task<ActionResult<Movie[]>> Get()
    {
        var movies = context.Movies;

        return await movies.ToArrayAsync();
    }

    [HttpPost(Name = "Post_Movie")]
    public async Task<ActionResult<MovieDTO>> Post(
        [FromBody] MovieDTO movieDTO)
    {
        var movie = context.Movies.Where(m => m.Name == movieDTO.Name).FirstOrDefault();

        if (movie == null)
        {
            movie = new Movie();
            movie.Name = movieDTO.Name;
            movie.Description = movieDTO.Description;
            movie.ReleaseDate = movieDTO.ReleaseDate;
            movie.Duration = movieDTO.Duration;
            context.Movies.Add(movie);
            context.SaveChanges();
            return Ok(movie);
        }
        return BadRequest(StatusCodes.Status405MethodNotAllowed);
    }
}
