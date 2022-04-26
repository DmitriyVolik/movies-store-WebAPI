using BLL.Models;
using BLL.Models.DTO;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using WebApiTasks.ActionFilters;
using WebApiTasks.Services;

namespace WebApiTasks.Controllers;

[ApiController]
[PerformanceActionFilter]
[Route("[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMovieRepository _moviesService;

    public MoviesController(IMovieRepository moviesService)
    {
        _moviesService = moviesService;
    }

    // [HttpGet]
    // public IActionResult Get()
    // {
    //     //return Ok(_moviesService.GetAllMovies());
    // }

    // [HttpGet("{id}")]
    // public IActionResult Get(Guid id)
    // {
    //     var movie = _moviesService.GetMovieById(id);
    //
    //     if (movie is null) return NotFound("Incorrect id");
    //
    //     return Ok(movie);
    // }

    [HttpPost]
    public IActionResult Post(MovieDTO movie)
    {
        _moviesService.AddMovie(movie);
        return Ok(movie);
    }

    // [HttpPatch("{id}")]
    // public IActionResult Patch(Guid id, Movie movie)
    // {
    //     try
    //     {
    //         _moviesService.UpdateMovie(id, movie);
    //     }
    //     catch (Exception e)
    //     {
    //         return NotFound();
    //     }
    //
    //     //movie.Id = id;
    //     return Ok(movie);
    // }

    // [HttpDelete("{id}")]
    // public IActionResult Delete(Guid id)
    // {
    //     try
    //     {
    //         _moviesService.DeleteMovie(id);
    //     }
    //     catch (Exception e)
    //     {
    //         return NotFound();
    //     }
    //
    //     return Ok();
    // }
}