using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Services.Abstractions;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using WebAPI.Controllers;
using Xunit;

namespace Tests.ControllersTests;

public class MoviesControllerTests
{
    private readonly IMoviesService _service;

    private readonly MoviesController _controller;

    private readonly IList<MovieModel> _movieModels;

    public MoviesControllerTests()
    {
        _service = A.Fake<IMoviesService>();
        ;
        _controller = new MoviesController(_service);
        _movieModels = A.CollectionOfDummy<MovieModel>(10);
        A.CallTo(() => _service.GetMovies()).Returns(_movieModels);
        A.CallTo(() => _service.GetMovieById(Guid.Empty)).Returns(_movieModels[0]);
    }

    [Fact]
    public void Get_AllMovies_MovieModels()
    {
        var result = _controller.Get();

        result.Count().Should().Be(_movieModels.Count);
    }

    [Fact]
    public void Get_MovieByIdCorrect_MovieModel()
    {
        var result = _controller.Get(Guid.Empty) as ObjectResult;
        var value = result!.Value;

        value.Should().BeEquivalentTo(_movieModels[0]);
    }

    [Fact]
    public void Get_MovieByIdCorrect_Status200()
    {
        var result = _controller.Get(Guid.Empty) as ObjectResult;

        result!.StatusCode.Should().Be(200);
    }

    [Fact]
    public void Get_MovieByIdIncorrect_Status404()
    {
        A.CallTo(() => _service.GetMovieById(Guid.Empty)).Returns(null);

        var result = _controller.Get(Guid.Empty) as ObjectResult;

        result!.StatusCode.Should().Be(404);
    }

    [Fact]
    public void Post_AddMovieCorrect_Status200()
    {
        var result = _controller.Post(_movieModels[0]) as ObjectResult;

        result!.StatusCode.Should().Be(200);
    }

    [Fact]
    public void Post_AddMovie_MovieModel()
    {
        var result = _controller.Post(_movieModels[0]) as ObjectResult;
        var value = result!.Value;

        value.Should().BeEquivalentTo(_movieModels[0]);
    }

    [Fact]
    public void Patch_UpdateMovie_Status200()
    {
        var result = _controller.Patch(Guid.Empty, _movieModels[0]) as ObjectResult;

        result!.StatusCode.Should().Be(200);
    }

    [Fact]
    public void Patch_UpdateMovie_MovieModel()
    {
        var result = _controller.Patch(Guid.Empty, _movieModels[0]) as ObjectResult;
        var value = result!.Value;

        value.Should().BeEquivalentTo(_movieModels[0]);
    }
}