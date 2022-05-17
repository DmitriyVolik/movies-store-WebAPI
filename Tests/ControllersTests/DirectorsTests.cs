using BLL.Services.Abstractions;
using DAL.Entities;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;
using Xunit;
using FluentAssertions;

namespace Tests.ControllersTests;

public class DirectorsTests
{
    [Fact]
    public void Add_Given_Director_Return_Status200()
    {
        var service = A.Fake<IDirectorsService>();
        var director = A.Dummy<Director>();
        var controller = new DirectorsController(service);
        
        var result = controller.Post(director) as ObjectResult;

        result!.StatusCode.Should().Be(200);
    }
}