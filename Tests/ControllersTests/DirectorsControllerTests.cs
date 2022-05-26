using BLL.Services.Abstractions;
using DAL.Entities;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;
using Xunit;

namespace Tests.ControllersTests;

public class DirectorsControllerTests
{
    private readonly IDirectorsService _service;

    private readonly Director _director;

    private readonly DirectorsController _controller;

    public DirectorsControllerTests()
    {
        _service = A.Fake<IDirectorsService>();
        _director = A.Dummy<Director>();
        _controller = new DirectorsController(_service);
    }

    [Fact]
    public void Post_AddDirector_Status200()
    {
        var result = _controller.Post(_director) as ObjectResult;

        result!.StatusCode.Should().Be(200);
    }

    [Fact]
    public void Post_AddDirector_Director()
    {
        var result = _controller.Post(_director) as ObjectResult;
        var value = result!.Value;

        value.Should().BeEquivalentTo(_director);
    }
}