using System;
using System.Collections.Generic;
using System.Linq;
using DAL.DB;
using DAL.Entities;
using DAL.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Models.Enums;
using Models.Exceptions;
using Models.Models;
using Xunit;

namespace Tests.RepositoriesTests;

public class MoviesRepositoryTests
{
    [Fact]
    public void Add_CorrectMovie_MovieWithCorrectId()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase("MoviesFakeDbForAddCorrectId")
            .Options;
        using var context = new Context(options);
        context.Directors.Add(Director);
        context.SaveChanges();
        var repository = new MoviesRepository(context);
        
        repository.Add(_movieModel);
        context.SaveChanges();

        context.Movies.First().Id.Should().NotBeEmpty();
    }
    
    [Fact]
    public void Add_CorrectMovie_MovieWithCorrectDirector()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase("MoviesFakeDbForAddCorrectDirector")
            .Options;
        using var context = new Context(options);
        context.Directors.Add(Director);
        context.SaveChanges();
        var repository = new MoviesRepository(context);
        
        repository.Add(_movieModel);
        context.SaveChanges();

        context.Movies.First().Director.Should().BeEquivalentTo(Director);
    }
    
    [Fact]
    public void Add_CorrectMovie_MovieWithCorrectGenres()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase("MoviesFakeDbForAddCorrectGenres")
            .Options;
        using var context = new Context(options);
        context.Directors.Add(Director);
        context.SaveChanges();
        var repository = new MoviesRepository(context);
        
        repository.Add(_movieModel);
        context.SaveChanges();

        context.Movies.First().Genres.Count.Should().Be(_movieModel.Genres.Count);
    }
    
    [Fact]
    public void Add_MovieWithIncorrectDirector_NotFoundException()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase("MoviesFakeDbForAddIncorrect")
            .Options;
        using var context = new Context(options);
        var repository = new MoviesRepository(context);

        var act = () => repository.Add(_movieModel);

        act.Should().Throw<IncorrectDataException>()
            .WithMessage("Incorrect director");
    }

    [Fact]
    public void Get_AllMovies_Movies()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase("MoviesFakeDbForGet")
            .Options;
        using var context = new Context(options);
        context.Directors.Add(Director);
        context.SaveChanges();
        var repository = new MoviesRepository(context);
        for (var i = 0; i < 5; i++)
        {
            context.Movies.Add(new Movie
            {
                Description = "Description",
                Title = "Title",
                Id = Guid.NewGuid(),
                Director = Director
            });
        }
        context.SaveChanges();

        var result = repository.Get();

        result.Count().Should().Be(5);
    }

    [Fact]
    public void GetById_CorrectMovieId_CorrectMovie()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase("MoviesFakeDbForGetById")
            .Options;
        using var context = new Context(options);
        context.Directors.Add(Director);
        context.Movies.Add(_movie);
        context.SaveChanges();
        var repository = new MoviesRepository(context);
        repository.Add(_movieModel);

        var result = repository.GetById(_movie.Id);

        result.Should().BeEquivalentTo(_movie);
    }

    [Fact]
    public void Update_CorrectUpdates_MovieWithUpdates()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase("MoviesFakeDbForUpdate")
            .Options;
        using var context = new Context(options);
        context.Add(Director);
        context.Add(_movie);
        context.SaveChanges();
        var updates = new MovieModel
        {
            Id = _movie.Id,
            Title = "NewTitle",
            Description = "NewDescription",
            ReleaseDate = DateTime.Now,
            Genres = new List<GenreEnum>
            {
                Capacity = 1
            }
        };
        var repository = new MoviesRepository(context);

        repository.Update(updates);
        context.SaveChanges();

        var movie = repository.GetById(_movie.Id);
        movie!.Title.Should().Be(updates.Title);
        movie!.Description.Should().Be(updates.Description);
        movie!.ReleaseDate.Should().Be(updates.ReleaseDate);
        movie!.Genres.Count.Should().Be(updates.Genres.Count);
    }
    
    [Fact]
    public void Update_IncorrectMovieId_NotFoundException()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase("MoviesFakeDbForUpdateIncorrect")
            .Options;
        using var context = new Context(options);
        var updates = new MovieModel
        {
            Id = _movie.Id,
            Title = "NewTitle",
            Description = "NewDescription",
            ReleaseDate = DateTime.Now,
            Genres = new List<GenreEnum>
            {
                Capacity = 1
            }
        };
        var repository = new MoviesRepository(context);

        var act = () => repository.Update(updates);

        act.Should().Throw<NotFoundException>()
            .WithMessage("Incorrect id");
    }

    [Fact]
    public void Delete_CorrectMovieId_NullMovie()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase("MoviesFakeDbForDelete")
            .Options;
        using var context = new Context(options);
        context.Add(Director);
        context.Add(_movie);
        context.SaveChanges();
        var repository = new MoviesRepository(context);

        repository.Delete(_movie.Id);
        context.SaveChanges();

        context.Movies.FirstOrDefault(x=>x.Id == _movie.Id)
            .Should().BeNull();
    }
    
    [Fact]
    public void Delete_IncorrectMovieId_NotFoundException()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase("MoviesFakeDbForDeleteIncorrect")
            .Options;
        using var context = new Context(options);
        var repository = new MoviesRepository(context);

        var act = () => repository.Delete(Guid.Empty);

        act.Should().Throw<NotFoundException>()
            .WithMessage("Incorrect id");
    }

    private readonly Movie _movie = new Movie
    {
        Id = Guid.NewGuid(),
        Title = "Title",
        Description = "Description",
        Director = Director,
        ReleaseDate = default,
    };

    private readonly MovieModel _movieModel = new MovieModel()
    {
        Title = "Title",
        Description = "Description",
        Director = "Director",
        ReleaseDate = default,
        Genres = new List<GenreEnum>
        {
            Capacity = 2
        }
    };

    private static readonly Director Director = new Director
    {
        FullName = "Director"
    };
}