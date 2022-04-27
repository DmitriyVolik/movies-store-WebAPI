using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using WebAPI.ActionFilters;
using MoviesService = BLL.Services.MoviesService;

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
    public IEnumerable<MovieDTO> Get()
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
    public IActionResult Post(MovieDTO movie)
    {
        try
        {
            _moviesService.AddMovie(movie);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
        return Ok(movie);
    }

    [HttpPatch("{id}")]
    public IActionResult Patch(Guid id, MovieDTO movie)
    {
        try
        {
            _moviesService.UpdateMovie(id, movie);
        }
        catch (Exception e)
        { 
            return NotFound(e.Message);
        }
        
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
            return NotFound();
        }
    
        return Ok();
    }
}