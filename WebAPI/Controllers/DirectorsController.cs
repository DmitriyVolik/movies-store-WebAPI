using System.Data.Entity.Validation;
using BLL.Services;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ActionFilters;

namespace WebAPI.Controllers;

[ApiController]
[PerformanceActionFilter]
[Route("[controller]")]
public class DirectorsController : ControllerBase
{
    private readonly DirectorsService _directorsService;

    public DirectorsController(DirectorsService directorsService)
    {
        _directorsService = directorsService;
    }

    [HttpPost]
    public IActionResult Post(Director director)
    {
        try
        {
            _directorsService.Add(director);
        }
        catch (Exception e)
        {
            return BadRequest(e.InnerException!.Message);
        }
        
        return Ok(director);
    }

}