using AutoMapper;
using BLL.Services.Abstractions;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using WebAPI.ActionFilters;
using WebAPI.Authorization.Services;
using WebAPI.Extensions;
using WebAPI.ViewModels;

namespace WebAPI.Controllers;

[ApiController]
[PerformanceActionFilter]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;

    private readonly IConfiguration _configuration;
    
    private readonly IMapper _mapper;  
    
    private readonly JwtService _jwtService;

    public UsersController(IUsersService usersService, JwtService jwtService, IConfiguration configuration, IMapper mapper)
    {
        _mapper = mapper;
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
    public IActionResult Post(UserRequestViewModel userRequest)
    {
        var userModel = _usersService.AddUser(_mapper.Map<User>(userRequest));

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