using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Services;
using BLL.Services.Abstractions;
using DAL.Entities;
using DAL.Repositories.Abstractions;
using FakeItEasy;
using FluentAssertions;
using Models.Models;
using Xunit;

namespace Tests.ServicesTests;

public class MoviesServiceTest
{
    private readonly List<Movie> _movies;
    
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly MoviesService _service;

    public MoviesServiceTest()
    {
        _unitOfWork = A.Fake<IUnitOfWork>();
        _service = new MoviesService(_unitOfWork);
        _movies= new List<Movie>
        {
            new Movie
            {
                Id = Guid.NewGuid(),
                Title = "Test",
                Description = "Description",
                Genres = new List<MovieGenre>(),
                Director = new Director
                {
                    Id = 1,
                    FullName = "Name"
                },
                ReleaseDate = DateTime.Now,
            },
            new Movie
            {
                Id = Guid.NewGuid(),
                Title = "Test",
                Description = "Description",
                Genres = new List<MovieGenre>(),
                Director = new Director
                {
                    Id = 2,
                    FullName = "Name"
                },
                ReleaseDate = DateTime.Now,
            },
            new Movie
            {
                Id = Guid.NewGuid(),
                Title = "Test",
                Description = "Description",
                Genres = new List<MovieGenre>(),
                Director = new Director
                {
                    Id = 3,
                    FullName = "Name"
                },
                ReleaseDate = DateTime.Now,
            },
        };
        A.CallTo(() => _unitOfWork.Movies.Get()).Returns(_movies);
        A.CallTo(() => _unitOfWork.Movies.GetById(Guid.Empty)).Returns(_movies[0]);
    }
    
    [Fact]
    public void GetMovies_AllMovies_CorrectCount()
    {
        var result = _service.GetMovies();

        result.Count().Should().Be(_movies.Count);
    }
    
    [Fact]
    public void GetMovieById_Movie_CorrectMovie()
    {
        var result = _service.GetMovieById(Guid.Empty);
        var movieExpected = _service.MovieToModel(_movies[0]);

        result.Should().BeEquivalentTo(movieExpected);
    }
}