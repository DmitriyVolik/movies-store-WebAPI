using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using WebAPI.ActionFilters;

namespace WebAPI.Controllers;

[ApiController]
[PerformanceActionFilter]
[Route("[controller]")]
public class MoviesController : ControllerBase
{
    private readonly MoviesService _moviesService;

    public MoviesController(MoviesService moviesService)
    {
        _moviesService = moviesService;
    }
    
    [HttpGet]
    [Authorize("movie:read")]
    public IEnumerable<MovieModel> Get()
    {
        return _moviesService.GetMovies();
    }

    [HttpGet("{id}")]
    [Authorize("movie:read")]
    public IActionResult Get(Guid id)
    {
        var movie = _moviesService.GetMovieById(id);

        if (movie is null) return NotFound("Incorrect id");

        return Ok(movie);
    }

    [HttpPost]
    [Authorize("movie:write")]
    public IActionResult Post(MovieModel movie)
    {
        _moviesService.AddMovie(movie);
        return Ok(movie);
    }

    [HttpPatch("{id}")]
    [Authorize("movie:write")]
    public IActionResult Patch(Guid id, MovieModel movie)
    {
        _moviesService.UpdateMovie(id, movie);
        return Ok(movie);
    }

    [HttpDelete("{id}")]
    [Authorize("movie:delete")]
    public void Delete(Guid id)
    {
        _moviesService.DeleteMovie(id);
    }
}