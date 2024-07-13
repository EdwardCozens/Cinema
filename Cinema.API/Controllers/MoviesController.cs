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
    public async Task<ActionResult<Movie[]>> Get()
    {
        var movies = context.Movies;

        return await movies.ToArrayAsync();
    }

    [HttpGet("{movieId}", Name = "Get_Movie")]
    public async Task<ActionResult<MovieDTO>> GetById(int? movieId)
    {
        var movie = context.Movies.Where(m => m.Id == movieId).FirstOrDefault();
        MovieDTO movieDTO;
        if (movie == null) {
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
        catch (Exception ex) {
            var x = ex;
        }
        //}
        return BadRequest(StatusCodes.Status405MethodNotAllowed);
    }
}
