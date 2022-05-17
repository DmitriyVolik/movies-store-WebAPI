using System.Linq;
using AutoMapper;
using BLL.Services.Abstractions;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Models.Models;
using WebAPI.Authorization.Services;
using WebAPI.Controllers;
using Xunit;

namespace Tests.ControllersTests;

public class UsersTests
{
    [Fact]
    public void Get_Return_UserModels()
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
}