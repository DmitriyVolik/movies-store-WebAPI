using System.Linq;
using AutoMapper;
using BLL.Services.Abstractions;
using DAL.Entities;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.Models;
using WebAPI.Authorization.Abstractions;
using WebAPI.Authorization.Services;
using WebAPI.Controllers;
using WebAPI.Extensions;
using WebAPI.Options;
using WebAPI.ViewModels;
using Xunit;

namespace Tests.ControllersTests;

public class UsersTests
{
    [Fact]
    public void Get_Users_UserModels()
    {
        var service = A.Fake<IUsersService>();
        var jwt = A.Fake<JwtService>();
        var configuration = A.Fake<IConfiguration>();
        var mapper = A.Fake<IMapper>();
        var users = A.CollectionOfDummy<UserModel>(10);
        var controller = new UsersController(service, jwt, configuration, mapper);
        A.CallTo(() => service.GetUsers()).Returns(users);

        var result = controller.Get();
        
        result!.Count().Should().Be(users.Count);
    }

    [Fact]
    public void Post_AddUser_Status200()
    {
        var service = A.Fake<IUsersService>();
        var jwt = A.Fake<IJwtService>();
        var configuration = A.Fake<IConfiguration>();
        var mapper = A.Fake<IMapper>();
        var userRequestViewModel = A.Dummy<UserRequestViewModel>();
        var controller = new UsersController(service, jwt, configuration, mapper);

        var result = controller.Post(userRequestViewModel) as ObjectResult;

        result!.StatusCode.Should().Be(200);
    }

    [Fact]
    public void Login_IncorrectEmail_Status401()
    {
        var service = A.Fake<IUsersService>();
        var jwt = A.Fake<IJwtService>();
        var configuration = A.Fake<IConfiguration>();
        var mapper = A.Fake<IMapper>();
        var loginModel = A.Dummy<LoginModel>();
        A.CallTo(() => service.GetUserByEmail(loginModel.Email)).Returns(null);
        var controller = new UsersController(service, jwt, configuration, mapper);
        
        var result = controller.Login(loginModel) as ObjectResult;
        
        result!.StatusCode.Should().Be(401);
    }
    
    [Fact]
    public void Login_IncorrectPassword_Status200()
    {
        var service = A.Fake<IUsersService>();
        var jwt = A.Fake<IJwtService>();
        var configuration = A.Fake<IConfiguration>();
        var mapper = A.Fake<IMapper>();
        var loginModel = A.Dummy<LoginModel>();
        var user = A.Dummy<User>();
        var hash = BCrypt.Net.BCrypt.HashPassword("");
        user.Password = hash;
        loginModel.Password = ".";
        A.CallTo(() => service.GetUserByEmail(loginModel.Email)).Returns(user);
        var controller = new UsersController(service, jwt, configuration, mapper);
        
        var result = controller.Login(loginModel) as ObjectResult;
        
        result!.StatusCode.Should().Be(401);
    }
    
    [Fact]
    public void Login_Correct_Status200()
    {
        var service = A.Fake<IUsersService>();
        var jwt = A.Fake<IJwtService>();
        var configuration = A.Fake<IConfiguration>();
        var mapper = A.Fake<IMapper>();
        var loginModel = A.Dummy<LoginModel>();
        var user = A.Dummy<User>();
        var hash = BCrypt.Net.BCrypt.HashPassword("");
        user.Password = hash;
        loginModel.Password = "";
        A.CallTo(() => service.GetUserByEmail(loginModel.Email)).Returns(user);
        var controller = new UsersController(service, jwt, configuration, mapper);
        
        var result = controller.Login(loginModel) as ObjectResult;
        
        result!.StatusCode.Should().Be(200);
    }
}