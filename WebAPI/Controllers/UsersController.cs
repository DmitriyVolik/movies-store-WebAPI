using BLL.Services;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using WebAPI.ActionFilters;
using WebAPI.Extensions;
using WebAPI.Services;

namespace WebAPI.Controllers;

[ApiController]
[PerformanceActionFilter]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly UsersService _usersService;

    private readonly IConfiguration _configuration;

    public UsersController(UsersService usersService, IConfiguration configuration)
    {
        _usersService = usersService;
        _configuration = configuration;
    }

    [HttpGet]
    public IEnumerable<UserModel> Get()
    {
        return _usersService.GetUsers();
    }

    [HttpPost]
    public IActionResult Post(User user)
    {
        var userModel = _usersService.AddUser(user);
        
        return Ok(JwtService.GetJwtResponse(userModel, _configuration.GetAuthConfiguration()));
    }
    
    [HttpPost("Actions/Login")]
    public IActionResult Login(LoginModel data)
    {
        var user = _usersService.GetByEmail(data.Email);

        if (user is null || !BCrypt.Net.BCrypt.Verify(data.Password, user.Password)) return Unauthorized("Incorrect login or password");

        return Ok(JwtService.GetJwtResponse(_usersService.UserToModel(user), _configuration.GetAuthConfiguration()));
    }
}