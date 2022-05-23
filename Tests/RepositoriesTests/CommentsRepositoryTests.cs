using System;
using System.Linq;
using DAL.DB;
using DAL.Entities;
using DAL.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Models.Exceptions;
using Xunit;

namespace Tests.RepositoriesTests;

public class CommentsRepositoryTests
{
    [Fact]
    public void Add_CorrectComment_CorrectCommentInDb()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase("CommentsFakeDbForAdd")
            .Options;
        using var context = new Context(options);
        context.Movies.Add(Movie);
        context.SaveChanges();
        var repository = new CommentsRepository(context);

        repository.Add(_comment);
        context.SaveChanges();

        context.Comments.First().Id.Should().NotBeEmpty();
    }
    
    [Fact]
    public void Add_IncorrectMovieId_CorrectCommentInDb()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase("CommentsFakeDbForAddIncorrect")
            .Options;
        using var context = new Context(options);
        context.SaveChanges();
        var repository = new CommentsRepository(context);
        
        var act = () => repository.Add(_comment);

        act.Should().Throw<IncorrectDataException>()
            .WithMessage("Incorrect movie_id");
    }
    
    private readonly Comment _comment = new Comment
    {
        MovieId = Movie.Id,
        Username = "Username",
        Body = "Body"
    };

    private static readonly Movie Movie = new Movie
    {
        Id = Guid.NewGuid(),
        Title = "Title",
        Description = "Description",
        ReleaseDate = default,
    };
}