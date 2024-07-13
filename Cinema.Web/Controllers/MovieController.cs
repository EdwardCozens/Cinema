using Microsoft.AspNetCore.Mvc;
using Cinema.DTOs;
using System.Text.Json;

namespace Cinema.Web.Controllers
{
    public class MovieController : Controller
    {
        private readonly IHttpClientFactory factory;

        public MovieController(IHttpClientFactory factory)
        {
            this.factory = factory;
        }
        public async Task<IActionResult> Index()
        {
            var httpClient = factory.CreateClient();
            // httpClient.BaseAddress = new Uri("https://localhost:7150/api/Movies");
            var response = await httpClient.GetAsync("https://localhost:7150/api/Movies");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var movies = JsonSerializer.Deserialize<List<MovieDTO>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(movies);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            var httpClient = factory.CreateClient();

            return View();
        }

        public async Task<IActionResult> Create()
        {
            var httpClient = factory.CreateClient();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieDTO movie)
        {

            if (ModelState.IsValid)
            {
                var httpClient = factory.CreateClient();
                var response = await httpClient.PostAsJsonAsync("https://localhost:7150/api/Movies", movie);
                response.EnsureSuccessStatusCode();
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
