using BLL.Services.Abstractions;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ActionFilters;

namespace WebAPI.Controllers;

[ApiController]
[PerformanceActionFilter]
[Route("[controller]")]
public class DirectorsController : ControllerBase
{
    private readonly IDirectorsService _directorsService;

    public DirectorsController(IDirectorsService directorsService)
    {
        _directorsService = directorsService;
    }

    [HttpPost]
    [Authorize("director:write")]
    public IActionResult Post(Director director)
    {
        _directorsService.AddDirector(director);
        return Ok(director);
    }

}