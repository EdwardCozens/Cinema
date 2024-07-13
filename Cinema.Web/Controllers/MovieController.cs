using Microsoft.AspNetCore.Mvc;
using Cinema.DTOs;
using System.Text.Json;

namespace Cinema.Web.Controllers;

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
        var response = await httpClient.GetAsync("https://localhost:7150/api/Movies");
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var movies = JsonSerializer.Deserialize<List<MovieDTO>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return View(movies);
    }

    public IActionResult Create()
    {
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

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var httpClient = factory.CreateClient();
        var response = await httpClient.GetAsync($"https://localhost:7150/api/Movies/{id}");
        var json = await response.Content.ReadAsStringAsync();
        var movie = JsonSerializer.Deserialize<MovieDTO>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return View(movie);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeletePost(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var httpClient = factory.CreateClient();
        var response = await httpClient.DeleteAsync($"https://localhost:7150/api/Movies/{id}");
        return RedirectToAction("Index");
    }
}
