using BLL.Services;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using WebAPI.ActionFilters;
using WebAPI.Authorization.Services;
using WebAPI.Extensions;

namespace WebAPI.Controllers;

[ApiController]
[PerformanceActionFilter]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly UsersService _usersService;

    private readonly IConfiguration _configuration;
    
    private readonly JwtService _jwtService;

    public UsersController(UsersService usersService, JwtService jwtService, IConfiguration configuration)
    {
        _usersService = usersService;
        _jwtService = jwtService;
        _configuration = configuration;
    }

    [HttpGet]
    [Authorize("user:read")]
    public IEnumerable<UserModel> Get()
    {
        return _usersService.GetUsers();
    }

    [HttpPost]
    public IActionResult Post(User user)
    {
        var userModel = _usersService.AddUser(user);

        return Ok(_jwtService.GetJwtResponse(userModel, _configuration.GetAuthConfiguration()));
    }

    [HttpPost("Actions/Login")]
    public IActionResult Login(LoginModel data)
    {
        var user = _usersService.GetUserByEmail(data.Email);

        if (user is null || !BCrypt.Net.BCrypt.Verify(data.Password, user.Password))
            return Unauthorized("Incorrect login or password");

        return Ok(_jwtService.GetJwtResponse(_usersService.UserToModel(user), _configuration.GetAuthConfiguration()));
    }
}