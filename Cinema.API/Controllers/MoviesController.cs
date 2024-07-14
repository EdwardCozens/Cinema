using AutoMapper;
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
    private readonly IMapper mapper;

    public MoviesController(ApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    [HttpGet(Name = "Get_Movies")]
    public async Task<ActionResult<MovieGetDTO[]>> Get()
    {
        var movies = await context.Movies.Include(m => m.Genre).ToArrayAsync();

        var movieDTOs = mapper.Map<MovieGetDTO[]>(movies);

        return movieDTOs;
    }

    [HttpGet("{movieId}", Name = "Get_Movie")]
    public async Task<ActionResult<MovieGetDTO>> GetById(int? movieId)
    {
        var movie = await context.Movies.Include(m => m.Genre).Where(m => m.Id == movieId).FirstOrDefaultAsync();
        MovieGetDTO movieDTO;
        if (movie != null)
        {
            movieDTO = mapper.Map<MovieGetDTO>(movie);
            return Ok(movieDTO);
        }

        return NotFound();
    }

    [HttpPost(Name = "Post_Movie")]
    public async Task<ActionResult<MovieDTO>> Post(
        [FromBody] MovieDTO movieDTO)
    {
        var movie = context.Movies.Where(m => m.Title == movieDTO.Title).FirstOrDefault();

        if (movie == null)
        {
            var newMovie = mapper.Map<Movie>(movieDTO);
            context.Movies.Add(newMovie);
            await context.SaveChangesAsync();
            return Ok(newMovie);
        }
        return BadRequest(StatusCodes.Status405MethodNotAllowed);
    }

    [HttpDelete("{movieId}", Name = "Delete_Movie")]
    public async Task<ActionResult> Delete(int? movieId)
    {
        var movie = await context.Movies.Where(m => m.Id == movieId).FirstOrDefaultAsync();
        if (movie != null)
        {
            context.Movies.Remove(movie);
            await context.SaveChangesAsync();
            return Ok();
        }
        return BadRequest();
    }

    [HttpPut(Name = "Put_Movie")]
    public async Task<ActionResult<MovieDTO>> Put(
        [FromBody] MovieDTO movieDTO)
    {
        var movie = await context.Movies.Where(m => m.Id == movieDTO.Id).FirstOrDefaultAsync();
        if (movie != null)
        {
            movie.Title = movieDTO.Title;
            movie.Description = movieDTO.Description;
            movie.ReleaseDate = movieDTO.ReleaseDate;
            movie.Duration = movieDTO.Duration;
            movie.GenreId = movieDTO.GenreId;
            context.Movies.Update(movie);
            await context.SaveChangesAsync();
            return Ok();
        }
        return BadRequest();
    }
}
