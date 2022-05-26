using System;
using System.Collections.Generic;
using System.Linq;
using DAL.DB;
using DAL.Entities;
using DAL.Repositories;
using FluentAssertions;
using Models.Enums;
using Models.Exceptions;
using Models.Models;
using Tests.RepositoriesTests.Helpers;
using Xunit;

namespace Tests.RepositoriesTests;

public class MoviesRepositoryTests
{
    private readonly Movie _movie;

    private readonly MovieModel _movieModel;

    private readonly Director _director;

    public MoviesRepositoryTests()
    {
        _director = new Director
        {
            FullName = "Director"
        };

        _movie = new Movie
        {
            Id = Guid.NewGuid(),
            Title = "Title",
            Description = "Description",
            Director = _director,
            ReleaseDate = default
        };

        _movieModel = new MovieModel
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
    }

    [Fact]
    public void Add_CorrectMovie_MovieWithCorrectId()
    {
        using var context = new Context(FakeDb.GetFakeDbOptions("MoviesFakeDbForAddCorrectId"));
        context.Directors.Add(_director);
        context.SaveChanges();
        var repository = new MoviesRepository(context);

        repository.Add(_movieModel);
        context.SaveChanges();

        context.Movies.First().Id.Should().NotBeEmpty();
    }

    [Fact]
    public void Add_CorrectMovie_MovieWithCorrectDirector()
    {
        using var context = new Context(FakeDb.GetFakeDbOptions("MoviesFakeDbForAddCorrectDirector"));
        context.Directors.Add(_director);
        context.SaveChanges();
        var repository = new MoviesRepository(context);

        repository.Add(_movieModel);
        context.SaveChanges();

        context.Movies.First().Director.Should().BeEquivalentTo(_director);
    }

    [Fact]
    public void Add_CorrectMovie_MovieWithCorrectGenres()
    {
        using var context = new Context(FakeDb.GetFakeDbOptions("MoviesFakeDbForAddCorrectGenres"));
        context.Directors.Add(_director);
        context.SaveChanges();
        var repository = new MoviesRepository(context);

        repository.Add(_movieModel);
        context.SaveChanges();

        context.Movies.First().Genres.Count.Should().Be(_movieModel.Genres.Count);
    }

    [Fact]
    public void Add_MovieWithIncorrectDirector_NotFoundException()
    {
        using var context = new Context(FakeDb.GetFakeDbOptions("MoviesFakeDbForAddIncorrect"));
        var repository = new MoviesRepository(context);

        var act = () => repository.Add(_movieModel);

        act.Should().Throw<IncorrectDataException>()
            .WithMessage("Incorrect director");
    }

    [Fact]
    public void Get_AllMovies_Movies()
    {
        using var context = new Context(FakeDb.GetFakeDbOptions("MoviesFakeDbForGet"));
        context.Directors.Add(_director);
        context.SaveChanges();
        var repository = new MoviesRepository(context);
        for (var i = 0; i < 5; i++)
            context.Movies.Add(new Movie
            {
                Description = "Description",
                Title = "Title",
                Id = Guid.NewGuid(),
                Director = _director
            });
        context.SaveChanges();

        var result = repository.Get();

        result.Count().Should().Be(5);
    }

    [Fact]
    public void GetById_CorrectMovieId_CorrectMovie()
    {
        using var context = new Context(FakeDb.GetFakeDbOptions("MoviesFakeDbForGetById"));
        context.Directors.Add(_director);
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
        using var context = new Context(FakeDb.GetFakeDbOptions("MoviesFakeDbForUpdate"));
        context.Add(_director);
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
        using var context = new Context(FakeDb.GetFakeDbOptions("MoviesFakeDbForUpdateIncorrect"));
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
        using var context = new Context(FakeDb.GetFakeDbOptions("MoviesFakeDbForDelete"));
        context.Add(_director);
        context.Add(_movie);
        context.SaveChanges();
        var repository = new MoviesRepository(context);

        repository.Delete(_movie.Id);
        context.SaveChanges();

        context.Movies.FirstOrDefault(x => x.Id == _movie.Id)
            .Should().BeNull();
    }

    [Fact]
    public void Delete_IncorrectMovieId_NotFoundException()
    {
        using var context = new Context(FakeDb.GetFakeDbOptions("MoviesFakeDbForDeleteIncorrect"));
        var repository = new MoviesRepository(context);

        var act = () => repository.Delete(Guid.Empty);

        act.Should().Throw<NotFoundException>()
            .WithMessage("Incorrect id");
    }
}