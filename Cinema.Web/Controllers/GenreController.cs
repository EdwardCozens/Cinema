using Cinema.DataAccess.Models;
using Cinema.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Cinema.Web.Controllers
{
    public class GenreController : Controller
    {
        private readonly IHttpClientFactory factory;

        public GenreController(IHttpClientFactory factory)
        {
            this.factory = factory;
        }

        public async Task<IActionResult> Index()
        {
            var httpClient = factory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:7150/api/Genres");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var genres = JsonSerializer.Deserialize<List<GenreDTO>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(genres);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GenrePostDTO genre)
        {

            if (ModelState.IsValid)
            {
                var httpClient = factory.CreateClient();
                var response = await httpClient.PostAsJsonAsync("https://localhost:7150/api/Genres", genre);
                response.EnsureSuccessStatusCode();
                return RedirectToAction("Index");
            }

            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var httpClient = factory.CreateClient();
            var response = await httpClient.GetAsync($"https://localhost:7150/api/Genres/{id}");
            var json = await response.Content.ReadAsStringAsync();
            var movie = JsonSerializer.Deserialize<GenreDTO>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GenreDTO genre)
        {
            if (ModelState.IsValid)
            {
                var httpClient = factory.CreateClient();
                var response = await httpClient.PutAsJsonAsync("https://localhost:7150/api/Genres", genre);
                response.EnsureSuccessStatusCode();
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var httpClient = factory.CreateClient();
            var response = await httpClient.GetAsync($"https://localhost:7150/api/Genres/{id}");
            var json = await response.Content.ReadAsStringAsync();
            var genre = JsonSerializer.Deserialize<GenreDTO>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(genre);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var httpClient = factory.CreateClient();
            var response = await httpClient.DeleteAsync($"https://localhost:7150/api/Genres/{id}");
            return RedirectToAction("Index");
        }
    }
}
