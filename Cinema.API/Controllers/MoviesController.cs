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
    public async Task<ActionResult<MovieDTO[]>> Get()
    {
        var movies = await context.Movies.ToArrayAsync();

        var movieDTOs = mapper.Map<MovieDTO[]>(movies);

        return movieDTOs;
    }

    [HttpGet("{movieId}", Name = "Get_Movie")]
    public async Task<ActionResult<MovieDTO>> GetById(int? movieId)
    {
        var movie = await context.Movies.Where(m => m.Id == movieId).FirstOrDefaultAsync();
        MovieDTO movieDTO;
        if (movie == null)
        {
            movieDTO = new MovieDTO();
        }
        else
        {
            movieDTO = mapper.Map<MovieDTO>(movie);
        }

        return movieDTO;
    }

    [HttpPost(Name = "Post_Movie")]
    public async Task<ActionResult<MovieDTO>> Post(
        [FromBody] MovieDTO movieDTO)
    {
        //  var movie = context.Movies.Where(m => m.Name == movieDTO.Name).FirstOrDefault();

        //if (movie == null)
        //{
        try
        {
            var movie = mapper.Map<Movie>(movieDTO);
            context.Movies.Add(movie);
            await context.SaveChangesAsync();
            return Ok(movie);
        }
        catch (Exception ex)
        {
            var x = ex;
        }
        //}
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
            context.Movies.Update(movie);
            await context.SaveChangesAsync();
            return Ok();
        }
        return BadRequest();
    }
}
