using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Services;
using DAL.Entities;
using DAL.Repositories.Abstractions;
using FakeItEasy;
using FluentAssertions;
using Models.Models;
using Xunit;

namespace Tests.ServicesTests;

public class MoviesServiceTest
{
    [Fact]
    public void GetMovies_AllMovies_CorrectCount()
    {
        var unitOfWork = A.Fake<IUnitOfWork>();
        A.CallTo(() => unitOfWork.Movies.Get()).Returns(_movies);
        var service = new MoviesService(unitOfWork);

        var result = service.GetMovies();

        result.Count().Should().Be(_movies.Count);
    }
    
    [Fact]
    public void GetMovieById_Movie_CorrectMovie()
    {
        var unitOfWork = A.Fake<IUnitOfWork>();
        A.CallTo(() => unitOfWork.Movies.GetById(Guid.Empty)).Returns(_movies[0]);
        var service = new MoviesService(unitOfWork);

        var result = service.GetMovieById(Guid.Empty);
        var movieExpected = service.MovieToModel(_movies[0]);

        result.Should().BeEquivalentTo(movieExpected);
    }
    
    private readonly List<Movie> _movies = new List<Movie>
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
}