using Cinema.DataAccess.Data;
using Cinema.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        public GenresController(ApplicationDbContext context)
        {
            Context = context;
        }

        public ApplicationDbContext Context { get; }

        [HttpGet(Name = "Get_Genres")]
        public async Task<ActionResult<Genre[]>> Get()
        {
            var categories = Context.Genres;

            return await categories.ToArrayAsync();
        }
    }
}
