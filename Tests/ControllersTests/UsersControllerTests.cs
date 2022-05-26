using System.Collections.Generic;
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
using WebAPI.Controllers;
using WebAPI.ViewModels;
using Xunit;

namespace Tests.ControllersTests;

public class UsersControllerTests
{
    private readonly IUsersService _service;

    private readonly UsersController _controller;

    private readonly IList<UserModel> _userModels;

    private readonly IJwtService _jwtService;

    private readonly IConfiguration _configuration;

    private readonly IMapper _mapper;

    private readonly UserRequestViewModel _userRequestViewModel;

    private readonly LoginModel _loginModel;

    private readonly User _user;

    private readonly string _hash;

    public UsersControllerTests()
    {
        _service = A.Fake<IUsersService>();
        ;
        _userModels = A.CollectionOfDummy<UserModel>(10);
        ;
        _jwtService = A.Fake<IJwtService>();
        _configuration = A.Fake<IConfiguration>();
        _mapper = A.Fake<IMapper>();
        _userRequestViewModel = A.Dummy<UserRequestViewModel>();
        _loginModel = A.Dummy<LoginModel>();
        _loginModel.Password = ".";
        _user = A.Dummy<User>();
        _hash = BCrypt.Net.BCrypt.HashPassword("");
        _user.Password = _hash;
        _controller = new UsersController(_service, _jwtService, _configuration, _mapper);
        A.CallTo(() => _service.GetUsers()).Returns(_userModels);
        A.CallTo(() => _service.GetUserByEmail(_loginModel.Email)).Returns(_user);
    }

    [Fact]
    public void Get_Users_UserModels()
    {
        var result = _controller.Get();

        result!.Count().Should().Be(_userModels.Count);
    }

    [Fact]
    public void Post_AddUserCorrect_Status200()
    {
        var result = _controller.Post(_userRequestViewModel) as ObjectResult;

        result!.StatusCode.Should().Be(200);
    }

    [Fact]
    public void Login_IncorrectEmail_Status401()
    {
        A.CallTo(() => _service.GetUserByEmail(_loginModel.Email)).Returns(null);

        var result = _controller.Login(_loginModel) as ObjectResult;

        result!.StatusCode.Should().Be(401);
    }

    [Fact]
    public void Login_IncorrectPassword_Status401()
    {
        var result = _controller.Login(_loginModel) as ObjectResult;

        result!.StatusCode.Should().Be(401);
    }

    [Fact]
    public void Login_Correct_Status200()
    {
        _loginModel.Password = "";

        var result = _controller.Login(_loginModel) as ObjectResult;

        result!.StatusCode.Should().Be(200);
    }
}