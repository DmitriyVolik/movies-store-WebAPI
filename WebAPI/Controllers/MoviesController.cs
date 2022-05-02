using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
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
    public IEnumerable<MovieModel> Get()
    {
        return _moviesService.GetMovies();
    }

    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        var movie = _moviesService.GetMovieById(id);

        if (movie is null) return NotFound("Incorrect id");

        return Ok(movie);
    }

    [HttpPost]
    public IActionResult Post(MovieModel movie)
    {
        _moviesService.AddMovie(movie);
        return Ok(movie);
    }

    [HttpPatch("{id}")]
    public IActionResult Patch(Guid id, MovieModel movie)
    {
        _moviesService.UpdateMovie(id, movie);
        return Ok(movie);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        try
        {
            _moviesService.DeleteMovie(id);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }

        return Ok();
    }
}