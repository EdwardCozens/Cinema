using AutoMapper;
using Cinema.DataAccess.Data;
using Cinema.DataAccess.Models;
using Cinema.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenresController : ControllerBase
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    public GenresController(ApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    [HttpGet(Name = "Get_Genres")]
    public async Task<ActionResult<GenreDTO[]>> Get()
    {
        var categories = await context.Genres.ToArrayAsync();

        return mapper.Map<GenreDTO[]>(categories);
    }

    [HttpGet("{genreId}", Name = "Get_Genre")]
    public async Task<ActionResult<GenreDTO>> GetById(int? genreId)
    {
        var genre = await context.Genres.Where(g => g.Id == genreId).FirstOrDefaultAsync();
        GenreDTO genreDTO;
        if (genre != null)
        {
            genreDTO = mapper.Map<GenreDTO>(genre);
            return genreDTO;
        }
        return BadRequest();
    }
    [HttpPost(Name = "Post_Genre")]
    public async Task<ActionResult> Post (
        [FromBody] GenrePostDTO genreDTO)
    {
        var genre = context.Genres.Where(g => g.Name == genreDTO.Name).FirstOrDefault();
        if (genre == null)
        { 
            var newGenre = mapper.Map<Genre>(genreDTO);
            context.Genres.Add(newGenre);
            await context.SaveChangesAsync();
            return Ok();
        }
        return BadRequest();
    }

    [HttpPut(Name = "Put_Genre")]
    public async Task<ActionResult> Post(
        [FromBody] GenreDTO genreDTO)
    {
        var genre = context.Genres.Where(g => g.Id == genreDTO.Id).FirstOrDefault();
        if (genre != null)
        {
            genre.Name = genreDTO.Name;
            context.Genres.Update(genre);
            await context.SaveChangesAsync();
            return Ok();
        }
        return BadRequest();
    }

    [HttpDelete("{genreId}", Name = "Delete_Genre")]
    public async Task<ActionResult> Delete(int? genreId)
    {
        var genre = await context.Genres.Where(m => m.Id == genreId).FirstOrDefaultAsync();
        if (genre != null)
        {
            context.Genres.Remove(genre);
            await context.SaveChangesAsync();
            return Ok();
        }
        return BadRequest();
    }
}
