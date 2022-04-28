using BLL.Services;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ActionFilters;

namespace WebAPI.Controllers;

[ApiController]
[PerformanceActionFilter]
[Route("[controller]")]
public class GenresController : ControllerBase
{
    private readonly GenresService _genresService;

    public GenresController(GenresService genresService)
    {
        _genresService = genresService;
    }

    [HttpPost]
    public IActionResult Post(Genre genre)
    {
        return NotFound();
    }
}